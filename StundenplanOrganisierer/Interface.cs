using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace StundenplanOrganisierer
{
    public partial class StuPlOrgInterface : Form
    {
        public StuPlOrgInterface()
        {
            InitializeComponent();
            
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            try
            {
                StreamReader file = new StreamReader(@"Konfiguration.txt");
                string line;
                DB.Text = "";
                while ((line = file.ReadLine()) != null)
                {
                    if (!line.StartsWith("##")&&line.Length!=0&&line.Contains(" "))
                    {
                        DB.Text = DB.Text + line + "\r\n";
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Fehlerfenster fenster = new Fehlerfenster("Es wurde keine Konfiguration gefunden\r\n\r\nEs sollte sich eine Konfiguration.txt neben der .exe befinden!\r\n\r\nAndernfalls wird die Standardkonfiguration verwendet");
                fenster.Show();
            }

            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 15000;
            toolTip1.InitialDelay = 700;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button1, "zerteilt die PDF in einzelne Seiten \r\n und sortiert sie nach den angegebenen Namen neu");
            toolTip1.SetToolTip(DB, "Tags und Klarnamen getrennt durch ein Leerzeichen\r\nDie Richtungen im Hauptstudium sind durch '+' gekennzeichnet, die Spezialisierungen mit '#'");
            toolTip1.SetToolTip(load, "lädt eine neue PDF");
            toolTip1.SetToolTip(Pfad, "Ort der aktuellen PDF");
            toolTip1.SetToolTip(OverrideBox, "Sollen PDFs, die bereits existieren überschrieben oder ergänzt werden?");

            Loadpdf();
            
            this.Activate();
        }


        /// <summary>
        /// erstellt neue Ordner für eine Liste von Namen
        /// </summary>
        /// <param name="names">Ordnernamen</param>
        /// <param name="path">Zielpfad</param>
        public void CreateFolders(List<string> names, string path)
        {
            foreach (var group in names)
            {
                Directory.CreateDirectory(path + "/" + group);
            }
            Directory.CreateDirectory(path + "/" + "Unzugeordnet");
        }

        /// <summary>
        /// erstellt einen Ordner
        /// </summary>
        /// <param name="names">Ordnernamen</param>
        /// <param name="path">Zielpfad</param>
        public void CreateFolder(string name, string path)
        {
            Directory.CreateDirectory(path + "/" + name);
        }

        /// <summary>
        /// Speichert einen String in eine existierende Pdf
        /// </summary>
        /// <param name="inputPdf">Original</param>
        /// <param name="pageSelection">Reichweite, z.B. "1-4" oder "1,3-4"</param>
        /// <param name="outputPdf">Zielpfad der Kopie</param>
        public void SaveToPdf(string inputPdf, int pageSelection, string outputPdf)
        {
            PdfReader reader = new PdfReader(inputPdf);
            PdfReader reader1 = new PdfReader(outputPdf);
            Document doc = new Document(reader.GetPageSizeWithRotation(1));
            PdfCopy copy = new PdfCopy(doc, new FileStream(outputPdf+"1",FileMode.Append));
            doc.Open();
            for (int i = 1; i <= reader1.NumberOfPages; i++)
            {
                copy.AddPage(copy.GetImportedPage(reader1, i));
            }
            copy.AddPage(copy.GetImportedPage(reader, pageSelection));
            reader.Close();
            reader1.Close();
            doc.Close();
            File.Delete(outputPdf);
            File.Move(outputPdf + "1", outputPdf);
        }

        /// <summary>
        /// Speichert bestimmte Seiten einer Pdf in eine Kopie
        /// </summary>
        /// <param name="inputPdf">Original</param>
        /// <param name="pageSelection">Reichweite, z.B. "1-4" oder "1,3-4"</param>
        /// <param name="outputPdf">Zielpfad der Kopie</param>
        public void CopySite(string inputPdf, int pageSelection, string outputPdf)
        {
            if (Directory.GetFiles(outputPdf.Substring(0, outputPdf.LastIndexOf("\\"))).Contains(outputPdf)
                &&!OverrideBox.Checked)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++HÄ?");
                SaveToPdf(inputPdf, pageSelection, outputPdf);
                return;
            }

            PdfReader reader = new PdfReader(inputPdf);
            FileStream fs = new FileStream(outputPdf, FileMode.Create);
            Document doc = new Document(reader.GetPageSizeWithRotation(1));
            PdfCopy copy = new PdfCopy(doc, fs);
            doc.Open();
            copy.AddPage(copy.GetImportedPage(reader, pageSelection));
            doc.Close();
        }
        /// <summary>
        /// löscht alle leeren Ordner und Unterordner an einem Ort
        /// </summary>
        /// <param name="startLocation">Zielordner</param>
        private static void ProcessDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }
        /// <summary>
        /// öffnet ein Dialogfenster, in dem eine Pdf ausgewählt werden kann
        /// </summary>
        private void Loadpdf ()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Pdf Files|*.pdf",
                Title = "Select a Pdf File"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                sr.Close();
            }
            Pfad.Text = openFileDialog1.FileName;       //Speicherort des Pfades
        }

        /// <summary>
        /// Magie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (Pfad.Text.Length == 0)
            {
                return;
            }
            DB.ReadOnly = true;     //sperrt Datenbank

            string loc = Pfad.Text;     
            string Source = loc.Substring(loc.LastIndexOf(@"\")+1); //bestimmt den Pfad, an dem die Pdf liegt
            loc = loc.Remove(loc.LastIndexOf(@"\")+1);      //bestimmt den Namen der Pdf

            Dictionary<string, string> haupt = new Dictionary<string, string>();
            Dictionary<string, string> grund = new Dictionary<string, string>();
            Dictionary<string, string> spez = new Dictionary<string, string>();


            string[] lines = DB.Text.Split('\n');       //übernimmt den Text aus dem Eingabefeld
            string lin;
            foreach (var line in lines)
            {
                if (line.Contains(" "))
                {
                    Console.Write(line);
                    lin = line.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");     //entfernt alle Umbrüche

                    if (lin.StartsWith("#"))
                    {
                        try
                        {
                            spez.Add(lin.Substring(1, lin.IndexOf(' ')-1), lin.Substring(lin.IndexOf(' ') + 1));
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Es gibt bereits " + lin.Substring(0, lin.IndexOf(' ')));
                        }
                    }
                    else if (lin.StartsWith("+"))
                    {
                        try
                        {
                            haupt.Add(lin.Substring(1, lin.IndexOf(' ') - 1), lin.Substring(lin.IndexOf(' ') + 1));
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Es gibt bereits " + lin.Substring(1, lin.IndexOf(' ') - 1));
                        }
                    }
                    else
                    {
                        try
                        {
                            grund.Add(lin.Substring(0, lin.IndexOf(' ')), lin.Substring(lin.IndexOf(' ') + 1));
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Es gibt bereits " + lin.Substring(1, lin.IndexOf(' ') - 1));
                        }
                    }
                }
            }

            var hauptlist = haupt.Keys.ToList();
            var grundlist = grund.Keys.ToList();
            var spezlist = spez.Keys.ToList();

            Console.WriteLine("----------------------------------------------------");
            spezlist.Sort(delegate(string x,string y) {
                if (x.Length == 0 && y.Length == 0) return 0;
                else if (x.Length == 0) return -1;
                else if (y.Length == 0) return 1;
                else return y.Length.CompareTo(x.Length);
            });
            Console.WriteLine("Spezialisierungen:");
            foreach (var item in spezlist)
            {
                Console.Write(" "+item);
            }

            grundlist.Sort(delegate (string x, string y) {
                if (x.Length == 0 && y.Length == 0) return 0;
                else if (x.Length == 0) return -1;
                else if (y.Length == 0) return 1;
                else return y.Length.CompareTo(x.Length);
            });
            Console.WriteLine("\r\nGrundstudium:");
            foreach (var item in grundlist)
            {
                Console.Write(" "+item);
            }

            hauptlist.Sort(delegate (string x, string y) {
                if (x.Length == 0 && y.Length == 0) return 0;
                else if (x.Length == 0) return -1;
                else if (y.Length == 0) return 1;
                else return y.Length.CompareTo(x.Length);
            });
            Console.WriteLine("\r\nHauptstudium:");
            foreach (var item in hauptlist)
            {
                Console.Write(" "+item);
            }
            Console.WriteLine("\r\n----------------------------------------------------");
            string output = string.Empty;

            CreateFolder("Unzugeordnet", loc);

            PdfReader reader = new PdfReader(loc + Source);
            int zahl = reader.NumberOfPages;

            for (int aktuelleSeite = 1; aktuelleSeite <= zahl; aktuelleSeite++)
            {
                from.Text = Convert.ToString(aktuelleSeite)+" von "+ Convert.ToString(zahl);
                progressBar.Value = aktuelleSeite / zahl * 100;

                string currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(aktuelleSeite)));

                string part = currentText.Substring(currentText.Length / 100 * Convert.ToInt32(positionlabel.Text.Substring(0, positionlabel.Text.IndexOf("-"))),
                    currentText.Length/100*Convert.ToInt32(positionlabel.Text.Substring(positionlabel.Text.IndexOf("-")+1, positionlabel.Text.Length - positionlabel.Text.IndexOf("-")-1)));   //String, der den Tag und den folgenden Block übernimmt

                string clear = "";
                while (part.Contains("(")
                    && (part.Contains(")Tj") || part.Contains(")]TJ")))      //solange chunks vorhanden sind
                {
                    int aufind = part.IndexOf("(");
                    int zuind = part.IndexOf(")", aufind);     //nimmt sich das auf "(" folgende ")"
                    clear = clear + part.Substring(aufind + 1, zuind - aufind - 1);        //nimmt sich den ersten Chunk -> (...)
                    part = part.Remove(0, zuind + 1);        // löscht den ersten chunk
                }
                Console.WriteLine(clear);
                

                bool success = false;

                foreach (var tag in grundlist)       //überprüft auf die Grundstudiums Tags
                {
                    int tagindex = clear.IndexOf(tag);
                    if (tagindex!=-1)       //enthält tag
                    {
                        Console.WriteLine("GRUNDSTUDIUM");
                        string plaintext = clear.Substring(tagindex, clear.IndexOf(" ", tagindex) - tagindex);   //String, der den Tag und den folgenden Block übernimmt

                        int id = grundlist.IndexOf(tag);
                        CreateFolder(grund[tag], loc);
                        output = loc + grund[tag] + "\\" + plaintext + ".pdf";
                        success = true;
                    }
                }

                foreach (var tag in hauptlist)       //Das Gleiche für das Hauptstudium, ein bisschen anders
                {
                    int tagindex = clear.IndexOf(tag);
                    if (tagindex != -1)       //enthält tag
                    {
                        Console.WriteLine("HAUPTSTUDIUM");
                        string plaintext = clear.Substring(tagindex, clear.IndexOf(" ",tagindex)-tagindex);   //String, der den Tag und den folgenden Block übernimmt
                        
                        int id = hauptlist.IndexOf(tag);
                        CreateFolder(haupt[tag], loc);
                        output = loc + haupt[tag] + "\\" + plaintext + ".pdf";
                        
                        foreach (var speztag in spezlist)
                        {
                            if (plaintext.Remove(plaintext.IndexOf(tag),tag.Length).Contains(speztag))      //Text ohne Tag
                            {
                                Console.WriteLine("SPEZIALISIERUNG");
                                Console.WriteLine(speztag);
                                CreateFolder(spez[speztag], loc+ "\\" + haupt[tag]);
                                output = loc + haupt[tag] + "\\" + spez[speztag] + "\\" + plaintext + ".pdf";
                                break;
                            }
                        }

                        success = true;
                    }
                }


                if (success == false)       //unzugeordnete Dateien
                {
                    output = loc + "Unzugeordnet\\" + "Plan" + aktuelleSeite.ToString("D2") + ".pdf";
                }

                CopySite(loc + Source, aktuelleSeite, output);      //speichert die fertige Datei

            }
            ProcessDirectory(loc);      //räumt auf
            DB.ReadOnly = false;        //gibt Datenbank wieder frei
        }
        
        private void Load_Click(object sender, EventArgs e)
        {
            Loadpdf();
        }

        private void Position_Scroll(object sender, EventArgs e)
        {
            switch (Position.Value)
            {
                case 1:
                    positionlabel.Text = "0-15";
                    break;
                case 2:
                    positionlabel.Text = "10-25";
                    break;
                case 3:
                    positionlabel.Text = "20-35";
                    break;
                case 4:
                    positionlabel.Text = "30-45";
                    break;
                case 5:
                    positionlabel.Text = "40-55";
                    break;
                case 6:
                    positionlabel.Text = "50-65";
                    break;
                case 7:
                    positionlabel.Text = "60-75";
                    break;
                case 8:
                    positionlabel.Text = "70-85";
                    break;
                default:
                    positionlabel.Text = "80-100";
                    break;
            }
        }
    }
}




/*
                int zahl = pdfReader.NumberOfPages + 1;
                for (int s = 1; s < zahl; s++)
                {
                    String ss = Convert.ToString(s);
                    Console.WriteLine(ss);
                    pdfReader.SelectPages("3-4");
                    var newFileStream = new FileStream(pfad + "/" + nameneu + s + ".pdf", FileMode.Create, FileAccess.Write);
                    var stamper = new PdfStamper(pdfReader, newFileStream);
                    stamper.Close();
                }
                //Reader.Close();
                pdfReader.Close();*/


/*
PdfReader reader = new PdfReader(pfad + "/" + name + ".pdf");
AcroFields.Item field = reader.AcroFields.Fields["[STOP_HERE]"];
if (field != null)
{
    int firstPage = reader.NumberOfPages + 1;
    for (int index = 0; index < field.Size; index++)
    {
        int page = field.GetPage(index);
        if (page > 0 && page < firstPage)
            firstPage = page;
    }

    if (firstPage <= reader.NumberOfPages)
    {
        reader.SelectPages("1-" + firstPage);
        PdfStamper stamper = new PdfStamper(reader, new FileStream(pfad + "/" + nameneu + ".pdf", FileMode.Create, FileAccess.Write));
        stamper.Close();
    }
}
reader.Close();
*/

/*
PdfWriter.GetInstance(doc, new FileStream(pfad + "/" + nameneu + ".pdf", FileMode.Create));
doc.Open();
Chunk c11 = new Chunk("Hier könnte ihre Werbung stehen");
c11.SetUnderline(0.5f,-1.5f);
for (int i = 1; i < 4; i++)
{doc.Add(new Paragraph(c11));}
doc.Close();
*/

/*          
try
{
    PdfWriter.GetInstance(doc2, new FileStream(pfad + "/Blocks2.pdf", FileMode.Create));
    doc2.Open();
    string text = @"Infantry gameplay has long been at the heart of our development. 
        Although it remains the backbone of our sandbox, we also recognise that there are several other avenues to explore. 
        By expanding upon combined-arms content and features, we aim to create fascinating new opportunities for our community, 
        and attract new players looking for a gateway to a massive military experience.
        We're also looking to make longer-term investments (significant free updates, for everyone) that, we hope, 
        help Arma 3 to continue to serve as platform for our community for years to come. To balance this effort - 
        plainly speaking: to fund our project - we've prepared a number of premium packages and free updates, which translate our vision into real development.";
    text = text.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);
    iTextSharp.text.Font georgia = FontFactory.GetFont("georgia", 10f);
    georgia.Color = iTextSharp.text.Color.GRAY;
    iTextSharp.text.Font lightblue = FontFactory.GetFont("georgia", 8f);
    lightblue.Color = iTextSharp.text.Color.BLUE;
    iTextSharp.text.Font courier = FontFactory.GetFont("courier", 9f);
    georgia.Color = iTextSharp.text.Color.GRAY;
    Chunk beginning = new Chunk(text, georgia);
    Phrase p1 = new Phrase(beginning);
    Chunk c1 = new Chunk("\n\nYou can of course force a newline using \"", georgia);
    Chunk c2 = new Chunk(@"\n", courier);
    Chunk c3 = new Chunk("\" or ", georgia);
    Chunk c4 = new Chunk("Environment", lightblue);
    Chunk c5 = new Chunk(".NewLine", courier);
    Chunk c6 = new Chunk(", or even ", georgia);
    Chunk c7 = new Chunk("Chunk", lightblue);
    Chunk c8 = new Chunk(".NEWLINE", courier);
    Chunk c9 = new Chunk(" as part of the string you give a chunk.", georgia);
    Phrase p2 = new Phrase();
    p2.Add(c1); p2.Add(c2); p2.Add(c3); p2.Add(c4); p2.Add(c5); p2.Add(c6); p2.Add(c7); p2.Add(c8); p2.Add(c9);
    Paragraph p = new Paragraph();
    p.Add(p1);
    p.Add(p2);
    p.SetAlignment("Justify");
    doc2.Add(p);
}
catch (DocumentException dex)
{throw (dex);}
catch (IOException ioex)
{throw (ioex);}
finally
{doc2.Close();}
*/

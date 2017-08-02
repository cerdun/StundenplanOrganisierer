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
            loadpdf();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

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
                System.IO.Directory.CreateDirectory(path + "/" + group);

            }
            System.IO.Directory.CreateDirectory(path + "/" + "Unzugeordnet");
        }
        /// <summary>
        /// Speichert einen String in eine Pdf
        /// </summary>
        /// <param name="input">Übergabestring</param>
        /// <param name="outputPdf">Zielpfad</param>
        public void SaveToPdf(string input, string outputPdf)
        {
            var doc = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(doc, new FileStream(outputPdf, FileMode.Create));
            doc.Open();
            Chunk c11 = new Chunk(input);
            doc.Add(new Paragraph(c11));
            doc.Close();
        }
        /// <summary>
        /// Speichert bestimmte Seiten einer Pdf in eine Kopie
        /// </summary>
        /// <param name="inputPdf">Original</param>
        /// <param name="pageSelection">Reichweite, z.B. "1-4" oder "1,3-4"</param>
        /// <param name="outputPdf">Zielpfad der Kopie</param>
        public void CopySite(string inputPdf, int pageSelection, string outputPdf)
        {
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
        private static void processDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                processDirectory(directory);
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
        private void loadpdf ()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pdf Files|*.pdf";
            openFileDialog1.Title = "Select a Pdf File";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                sr.Close();
            }
            Pfad.Text = openFileDialog1.FileName;       //Speicherort des Pfades
        }

        /// <summary>
        /// Magie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Pfad.Text.Length == 0)
            {
                return;
            }

            string loc = Pfad.Text;     
            string Source = loc.Substring(loc.LastIndexOf(@"\")+1); //bestimmt den Pfad, an dem die Pdf liegt
            loc = loc.Remove(loc.LastIndexOf(@"\")+1);      //bestimmt den Namen der Pdf

            
            List<string> tags = new List<string>();
            List<string> groups = new List<string>();

            string[] lines = DB.Text.Split('\n');       //übernimmt den Text aus dem Eingabefeld
            string lin;
            foreach (var line in lines)
            {
                lin = line.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");     //entfernt alle Umbrüche (hat ein wenig gedauert herauszufinden, wie^^)
                tags.Add(lin.Substring(0, lin.IndexOf(' ')));           //holt sich den Tag
                groups.Add(lin.Substring(lin.IndexOf(' ')+1));          //holt sich die Bezeichnung
            }
            
            string output = string.Empty;
            int[] counter = new int[groups.Count()];            //erstellt einen Zähler, beginnt für jede Gruppe mit 1
            for (int i = 0; i < counter.Count(); i++)
            {
                counter[i] = 1;
            }

            CreateFolders(groups, loc);

            PdfReader reader = new PdfReader(loc + Source);
            int zahl = reader.NumberOfPages;

            for (int i = 1; i <= zahl; i++)
            {
                from.Text = Convert.ToString(i)+" von "+ Convert.ToString(zahl);
                progressBar1.Value = i / zahl * 100;
                string currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(i)));

                bool success = false;
                foreach (var tag in tags)
                {
                    if (currentText.Contains(tag))
                    {
                        int id = tags.IndexOf(tag);
                        output = loc + groups[id] + "/" + groups[id] + " " + counter[id].ToString("D2") + ".pdf";       //D2=länge  2->02
                        counter[id]++;
                        success = true;
                    }
                }
                if (success == false)
                {
                    output = loc + "Unzugeordnet/" + "Plan" + i.ToString("D2") + ".pdf";
                }

                CopySite(loc + Source, i, output);
            }
            processDirectory(loc);
            
        }
        
        private void load_Click(object sender, EventArgs e)
        {
            loadpdf();
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

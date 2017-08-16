using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        functions _F = new functions();

        public StuPlOrgInterface()
        {
            InitializeComponent();
            
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            try
            {
                StreamReader file = new StreamReader(@"Konfiguration.txt");     //liest Konfig aus
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

            ToolTip toolTip1 = new ToolTip();           //Tooltip Stuff
            toolTip1.AutoPopDelay = 15000;
            toolTip1.InitialDelay = 700;
            toolTip1.ReshowDelay = 500;
            toolTip1.SetToolTip(button1, "zerteilt die PDF(s) in einzelne Seiten \r\n und sortiert sie nach den angegebenen Namen neu");
            toolTip1.SetToolTip(DB, "Tags und Klarnamen getrennt durch ein Leerzeichen\r\nDie Richtungen im Hauptstudium sind durch '+' gekennzeichnet, die Spezialisierungen mit '#'");
            toolTip1.SetToolTip(load, "lädt eine neue PDF");
            toolTip1.SetToolTip(Pfad, "Ort der aktuellen PDF");
            toolTip1.SetToolTip(OverrideBox, "Sollen PDFs, die bereits existieren, überschrieben oder ergänzt werden?");
            toolTip1.SetToolTip(ziel, "Ort, an dem die fertigen Dateien (in Unterordnern) gespeichert werden");
            toolTip1.SetToolTip(save, "Ändert den Speicherort");
            toolTip1.SetToolTip(checkBoxAllPdf, "Sollen alle PDFs am Speicherort der ausgewählten Datei aufgeteilt werden?");
            toolTip1.SetToolTip(snippet, "Zeigt den ausgewählten Ausschnitt von der ersten Seite der ausgewählten Pdf an");
            toolTip1.SetToolTip(opendesti, "Öffnet den Zielordner im Explorer");

            Pfad.Text = Loadpdf();          //liest beim starten der anwendung den pfad einer pdf
            ziel.Text = Pfad.Text.Substring(0, Pfad.Text.LastIndexOf(@"\"));

            this.Activate();
        }

        /// <summary>
        /// öffnet ein Dialogfenster, in dem eine Pdf ausgewählt werden kann
        /// </summary>
        /// <returns>Ort der Pdf</returns>
        private string Loadpdf ()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Pdf Files|*.pdf",
                Title = "Select a Pdf File"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return ""; 
        }

        /// <summary>
        /// Zerteilen der Pdf(s)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string time = DateTime.Now.ToString().Replace(" ", "_").Replace(":", ".");
                Console.WriteLine(time);
                if (checkBoxAllPdf.Checked)         //für alle pdfs im ordner?
                {
                    foreach (var file in Directory.GetFiles(Pfad.Text.Substring(0, Pfad.Text.LastIndexOf(@"\") + 1)))
                    {
                        if (file.EndsWith(".pdf"))
                        {
                            zerteilen(file, time);
                        }
                    }
                    return;
                }
                zerteilen(Pfad.Text, time);     //für nur eine pdf
            }
            catch (Exception baum)
            {
                MessageBox.Show("Etwas ist schief gelaufen: "+baum);
            }
        }

        /// <summary>
        /// Laden einer Pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load_Click(object sender, EventArgs e)
        {
            Pfad.Text = Loadpdf();
        }

        /// <summary>
        /// Einstellen der Position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Ändern des Speicherorts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ziel.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        /// <summary>
        /// irgendwas mit Magie
        /// </summary>
        /// <param name="filename">Ausgangsdatei</param>
        private void zerteilen(string Source, string time)
        {
            if (!Directory.Exists(ziel.Text)||!File.Exists(Source))
            {
                MessageBox.Show("Ungültiger Pfad");
                return;
            }

            DB.ReadOnly = true;     //sperrt Datenbank
            string loc = ziel.Text + "\\" + time + "\\";      //bestimmt den Ort der Ziel Pdf

            Dictionary<string, string> haupt = new Dictionary<string, string>();
            Dictionary<string, string> grund = new Dictionary<string, string>();
            //Dictionary<string, string> spez = new Dictionary<string, string>();     //optional für Sortierung nach Spezialisierung


            string[] lines = DB.Text.Split('\n');       //übernimmt den Text aus dem Eingabefeld
            string lin;
            Console.WriteLine("----------------------------------------------------");
            foreach (var line in lines)
            {
                if (line.Contains(" "))
                {
                    Console.Write(line);
                    lin = line.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");     //entfernt alle Umbrüche

                    if (lin.StartsWith("+"))
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
                    /*
                    else if (lin.StartsWith("#"))
                    {
                        try
                        {
                            spez.Add(lin.Substring(1, lin.IndexOf(' ') - 1), lin.Substring(lin.IndexOf(' ') + 1));
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Es gibt bereits " + lin.Substring(0, lin.IndexOf(' ')));
                        }
                    }*/
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
            //var spezlist = spez.Keys.ToList();

            Console.WriteLine("----------------------------------------------------");
            grundlist.Sort(delegate (string x, string y) {       //überschreibt Sortieralgorithmus
                if (x.Length == 0 && y.Length == 0) return 0;   //leere Keys
                else if (x.Length == 0) return -1;              //erster Key leer
                else if (y.Length == 0) return 1;               //zweiter Key leer
                else return y.Length.CompareTo(x.Length);       //Vergleich über Länge
            });
            Console.WriteLine("\r\nGrundstudium:");
            foreach (var item in grundlist)
                Console.Write(" " + item);
            /*
            spezlist.Sort(delegate (string x, string y) {
                if (x.Length == 0 && y.Length == 0) return 0;
                else if (x.Length == 0) return -1;
                else if (y.Length == 0) return 1;
                else return y.Length.CompareTo(x.Length);
            });
            Console.WriteLine("Spezialisierungen:");
            foreach (var item in spezlist)
                Console.Write(" " + item);
            */
            hauptlist.Sort(delegate (string x, string y) {
                if (x.Length == 0 && y.Length == 0) return 0;
                else if (x.Length == 0) return -1;
                else if (y.Length == 0) return 1;
                else return y.Length.CompareTo(x.Length);
            });
            Console.WriteLine("\r\nHauptstudium:");
            foreach (var item in hauptlist)
                Console.Write(" " + item);
            Console.WriteLine("\r\n----------------------------------------------------");

            string output = string.Empty;
            
            _F.CreateFolders("Unzugeordnet", loc);

            PdfReader reader = new PdfReader(Source);
            int zahl = reader.NumberOfPages;
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            
            for (int aktuelleSeite = 1; aktuelleSeite <= zahl; aktuelleSeite++)
            {
                from.Text = Convert.ToString(aktuelleSeite) + " von " + Convert.ToString(zahl);
                progressBar.Value = aktuelleSeite / zahl * 100;

                string clear = _F.ClearText(Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(aktuelleSeite))), positionlabel.Text);
                Console.WriteLine(clear);


                bool success = false;

                foreach (var tag in grundlist)       //überprüft auf die Grundstudiums Tags
                {
                    int tagindex = clear.IndexOf(tag);
                    if (tagindex != -1)       //enthält tag
                    {
                        Console.Write("GRUNDSTUDIUM");
                        string plaintext = clear.Substring(tagindex, clear.IndexOf(" ", tagindex) - tagindex);   //String, der den Tag und den folgenden Block übernimmt

                        string group = plaintext.Remove(0, plaintext.LastIndexOf("-")+1);
                        string semester = "semester"+plaintext.ElementAt(plaintext.IndexOfAny(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' })).ToString();
                        _F.CreateFolders(semester, loc);
                        _F.CreateFolders(grund[tag], loc + "\\" + semester);
                        output = loc + semester + "\\" + grund[tag] + "\\" + group + ".pdf";

                        success = true;
                    }
                }

                foreach (var tag in hauptlist)       //Das Gleiche für das Hauptstudium, ein bisschen anders
                {
                    int tagindex = clear.IndexOf(tag);
                    if (tagindex != -1)       //enthält tag
                    {
                        Console.Write("HAUPTSTUDIUM");
                        string plaintext = clear.Substring(tagindex, clear.IndexOf(" ", tagindex) - tagindex);   //String, der den Tag und den folgenden Block übernimmt

                        string group = plaintext.Remove(0, plaintext.LastIndexOf("-") + 1);
                        string semester = "semester" + plaintext.ElementAt(plaintext.IndexOfAny(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' })).ToString();
                        _F.CreateFolders(semester, loc);
                        _F.CreateFolders(haupt[tag], loc + "\\" + semester);
                        output = loc + semester + "\\" + haupt[tag] + "\\" + group + ".pdf";

                        // Möglichkeit die Spezialisierungen in eigene Ordner zu speichern
                        /*
                        output = loc + haupt[tag] + "\\" + plaintext + ".pdf";
                        foreach (var speztag in spezlist)
                        {
                            if (plaintext.Remove(plaintext.IndexOf(tag), tag.Length).Contains(speztag))      //Text ohne Tag
                            {
                                Console.WriteLine("SPEZIALISIERUNG");
                                Console.WriteLine(speztag);
                                _F.CreateFolders(spez[speztag], loc + "\\" + haupt[tag]);
                                output = loc + haupt[tag] + "\\" + spez[speztag] + "\\" + plaintext + ".pdf";
                                break;
                            }
                        }*/

                        success = true;
                    }
                }


                if (success == false)       //unzugeordnete Dateien
                {
                    output = loc + "Unzugeordnet\\" + Source.Substring(Source.LastIndexOf("\\"));
                }

                _F.CopySite(Source, aktuelleSeite, output, OverrideBox.Checked);      //speichert die fertige Datei
                Console.WriteLine("");
            }
            
            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            _F.ProcessDirectory(loc);      //räumt auf
            DB.ReadOnly = false;        //gibt Datenbank wieder frei
        }

        /// <summary>
        /// Gibt den ausgewählten ausschnitt aus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void snippet_Click(object sender, EventArgs e)
        {
            try
            {
                PdfReader reader = new PdfReader(Pfad.Text);

                string clear = _F.ClearText(Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(1))), positionlabel.Text);

                MessageBox.Show(clear);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Etwas ist schief gelaufen: "+exc);
            }
        }

        /// <summary>
        /// Öffnet den Zielordner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opendesti_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", ziel.Text);           //einziger Grund, warum System.Diagnostics genutzt wird
            }
            catch (Exception abc)
            {
                MessageBox.Show("Etwas ist schief gelaufen: " +abc);
            }
        }

        /// <summary>
        /// Benennt den zuletzt erstellten Ordner nach dem Semester um
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rename_Click(object sender, EventArgs e)
        {
            try
            {
                PdfReader reader = new PdfReader(Pfad.Text);
                string clear = _F.ClearText(Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, reader.GetPageContent(1))), positionlabel.Text);
                string sem = "";
                int semind = -1;
                if (clear.Contains("SoSe"))
                {
                    sem = "sommersemester_";
                    semind = clear.IndexOf("SoSe");
                }
                else if (clear.Contains("WiSe"))
                {
                    sem = "wintersemester_";
                    semind = clear.IndexOf("WiSe");
                }
                else
                    return;

                Console.WriteLine(semind);
                int start = clear.IndexOf("20", semind);
                string year = clear.Substring(start + 2, clear.IndexOf(" ", start) - start - 2);

                string target = "";
                string loc = ziel.Text;
                foreach (var dir in Directory.GetDirectories(loc))
                {
                    if (!Directory.Exists(target)||Directory.GetCreationTime(target)<Directory.GetCreationTime(dir))
                    {
                        target = dir;
                    }
                }
                Directory.Move(target, loc + "\\" + sem + year);
            }
            catch (Exception xy)
            {
                MessageBox.Show("Etwas ist schief gelaufen: "+xy);
            }
        }


        /// <summary>
        /// lädte eine andere konfig
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string desti="";
                OpenFileDialog openFileDialog1 = new OpenFileDialog()
                {
                    Filter = "txt Files|*.txt",
                    Title = "Select a txt File"
                };
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    desti= openFileDialog1.FileName;
                }
                StreamReader file = new StreamReader(desti);     //liest Konfig aus
                string line = "";
                DB.Text = "";
                while ((line = file.ReadLine()) != null)
                {
                    if (!line.StartsWith("##") && line.Length != 0 && line.Contains(" "))
                    {
                        DB.Text = DB.Text + line + "\r\n";
                    }
                }
            }
            catch (Exception argh)
            {
                MessageBox.Show("Etwas ist schief gelaufen: "+argh);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace StundenplanOrganisierer
{
    class functions
    {
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
        /// erstellt nur einen Order
        /// </summary>
        /// <param name="names">Ordnernamen</param>
        /// <param name="path">Zielpfad</param>
        public void CreateFolders(string name, string path)
        {
            Directory.CreateDirectory(path + "/" + name);
        }

        /// <summary>
        /// löscht alle leeren Ordner und Unterordner an einem Ort
        /// </summary>
        /// <param name="startLocation">Zielordner</param>
        public void ProcessDirectory(string startLocation)
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
        /// Speichert bestimmte Seiten einer Pdf in eine Kopie
        /// </summary>
        /// <param name="inputPdf">Original</param>
        /// <param name="pageSelection">Reichweite, z.B. "1-4" oder "1,3-4"</param>
        /// <param name="outputPdf">Zielpfad der Kopie</param>
        public void CopySite(string inputPdf, int pageSelection, string outputPdf, bool overri)
        {
            Console.WriteLine("... Wird hier gespeichert: " + outputPdf.Substring(0, outputPdf.LastIndexOf("\\")));
            if (Directory.GetFiles(outputPdf.Substring(0, outputPdf.LastIndexOf("\\"))).Contains(outputPdf)
                && !overri)
            {
                Console.WriteLine("Datei wird ergänzt");
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
            PdfCopy copy = new PdfCopy(doc, new FileStream(outputPdf + "1", FileMode.Append));
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
        /// räumt einen string auf
        /// </summary>
        /// <param name="currentText">auszuwertender string</param>
        /// <param name="position">zu betrachtender Bereich</param>
        /// <returns>string nur mit inhalt der einzelnen chuncks</returns>
        public string ClearText(string currentText, string position)
        {
            string part = currentText.Substring(currentText.Length / 100 * Convert.ToInt32(position.Substring(0, position.IndexOf("-"))),
                    currentText.Length / 100 * Convert.ToInt32(position.Substring(position.IndexOf("-") + 1, position.Length - position.IndexOf("-") - 1)));   //String, der den Tag und den folgenden Block übernimmt

            string clear = "";

            while (part.Contains("(")
                && (part.Contains(")Tj") || part.Contains(")]TJ") || part.Contains(")] TJ") || part.Contains(") TJ")))      //solange chunks vorhanden sind
            {
                int aufind = part.IndexOf("(");
                int zuind = part.IndexOf(")", aufind);     //nimmt sich das auf "(" folgende ")"
                clear = clear + part.Substring(aufind + 1, zuind - aufind - 1);        //nimmt sich den ersten Chunk -> (...)
                part = part.Remove(0, zuind + 1);        // löscht den ersten chunk
            }
            return clear;
        }
    }
}

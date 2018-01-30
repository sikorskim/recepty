using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

namespace recepty
{
    public class PulaRecept
    {
        public int PulaReceptId { get; private set; }
        public string Typ { get; private set; }
        public string Kategoria { get; private set; }
        public string NrPrawa { get; private set; }
        public string PESEL { get; private set; }
        public string ImieNazwisko { get; private set; }
        public int IloscRecept { get; private set; }

        public PulaRecept() { }

        public PulaRecept(string typ, string kategoria, string nrPrawa, string pesel, string imieNazwisko, int iloscRecept)
        {
            Typ = typ;
            Kategoria = kategoria;
            NrPrawa = nrPrawa;
            PESEL = pesel;
            ImieNazwisko = imieNazwisko;
            IloscRecept = iloscRecept;
        }

        static bool insertToDb(PulaRecept pulaRecept, List<PrescriptionNumber> nrReceptyList)
        {
            try
            {
                Model1 model = new Model1();
                model.PulaRecept.Add(pulaRecept);
                model.PrescriptionNumber.AddRange(nrReceptyList);
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool importFromXML(string path)
        {
            try
            {
                string filename = unzip(path);
                string tempDir = "temp";
                XDocument doc = XDocument.Load(tempDir + @"\" + filename);
                List<PrescriptionNumber> nrReceptyList = new List<PrescriptionNumber>();

                XElement elemRec = doc.Element("recepty");
                XElement elemLek = doc.Element("recepty").Element("lekarz");

                string typ = elemRec.Attribute("typ").Value;
                string kategoria = elemRec.Attribute("kat").Value;
                string imieNazwisko = elemLek.Attribute("imie_nazw").Value;
                string pesel = elemLek.Attribute("pesel").Value;
                string nrPrawa = elemLek.Attribute("pr_zawod").Value;
                int iloscRecept = Int32.Parse(elemLek.Attribute("il_rec").Value);

                PulaRecept pulaRecept = new PulaRecept(typ, kategoria, nrPrawa, pesel, imieNazwisko, iloscRecept);
                //int count=countElements(path);
                //int i = 0;
                foreach (XElement elem in elemLek.Elements("n"))
                {
                    string numer = elem.Value;
                    PrescriptionNumber nrRecepty = new PrescriptionNumber(numer, pulaRecept);
                    nrReceptyList.Add(nrRecepty);
                   //Console.WriteLine(i.ToString()+@"/"+count.ToString());
                }

                insertToDb(pulaRecept, nrReceptyList);
                deleteTemporaryFiles();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        static string unzip(string path)
        {
            string tempDir = "temp";
            try
            {
                ZipFile.ExtractToDirectory(path, tempDir);
                return getFilename(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        static string getFilename(string path)
        {
            int startChar = path.LastIndexOf(@"\") + 1;
            string filename = path.Substring(startChar, path.Length - startChar);
            filename = filename.TrimEnd('z');
            filename += 'l';
            return filename;
        }

        public static void deleteTemporaryFiles()
        {
            try
            {
                string path = "temp";
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception e)
            {
                Diagnostics.insertToDb(new Diagnostics("PulaRecept.deleteTemporaryFiles()", e.ToString()));
            }
        }


        static bool checkLekarzData(PulaRecept pulaRecept, Doctor lekarz)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static List<PulaRecept> getAll()
        {
            Model1 model = new Model1();
            return model.PulaRecept.ToList();
        }

        public static int countElements(string path)
        {
            XDocument doc = XDocument.Load(path);
            int count = doc.Element("recepty").Element("lekarz").Elements("n").Count();
            return count;
        }
    }
}
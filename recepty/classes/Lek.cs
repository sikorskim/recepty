using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace recepty
{
    public class Lek
    {
        [Key]
        public string BL7 { get; private set; }
        public string EAN { get; private set; }
        public bool Psychotrop { get; private set; }
        public bool Senior { get; private set; }
        public bool Szczepionka { get; private set; }
        public decimal Cena { get; private set; }
        public string Nazwa { get; private set; }
        public string NazwaInt { get; private set; }
        public string Postac { get; private set; }
        public string Dawka { get; private set; }
        public string Opakowanie { get; private set; }
        public bool Active { get; private set; }

        public Lek() { }

        public Lek(string bl7, string ean, bool psychotrop, bool senior, bool szczepionka, decimal cena, string nazwa,
            string nazwaInt, string postac, string dawka, string opakowanie)
        {
            BL7 = bl7;
            EAN = ean;
            Psychotrop = psychotrop;
            Senior = senior;
            Szczepionka = szczepionka;
            Cena = cena;
            Nazwa = nazwa;
            NazwaInt = nazwaInt;
            Postac = postac;
            Dawka = dawka;
            Opakowanie = opakowanie;
            Active = true;
        }

        static bool insertToDb(List<Lek> lekiList, List<Refundacja> refundacjaList)
        {
            try
            {
                Model1 model = new Model1();
                model.Lek.AddRange(lekiList);
                model.Refundacja.AddRange(refundacjaList);
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
                XDocument doc = XDocument.Load(path);
                List<Lek> lekiList = new List<Lek>();
                List<Refundacja> refundacjaList = new List<Refundacja>();

                foreach (XElement elem in doc.Element("Leki").Elements("Lek"))
                {
                    string bl7 = elem.Attribute("BL7").Value;
                    string ean = elem.Attribute("EAN").Value;
                    bool psychotrop = Boolean.Parse(elem.Attribute("psychotrop").Value);
                    bool senior = Boolean.Parse(elem.Attribute("senior").Value);
                    bool szczepionka = Boolean.Parse(elem.Attribute("szczepionka").Value);
                    decimal cena = Decimal.Parse(elem.Attribute("cena").Value);
                    string nazwa = elem.Element("Nazwa").Value;
                    string nazwaInt = elem.Element("NazwaInt").Value;
                    string postac = elem.Element("Postać").Value;
                    string dawka = elem.Element("Dawka").Value;
                    string opakowanie = elem.Element("Opakowanie").Value;

                    Lek lek = new Lek(bl7, ean, psychotrop, senior, szczepionka, cena, nazwa, nazwaInt, postac, dawka,
                        opakowanie);
                    lekiList.Add(lek);

                    if (elem.Element("Refundacja").HasElements)
                    {
                        foreach (XElement elemRef in elem.Element("Refundacja").Elements("Poziom"))
                        {
                            string poziom = elemRef.Attribute("poziom").Value;
                            string tekst = elemRef.Value;

                            Refundacja refundacja = new Refundacja(poziom, tekst, lek);
                            refundacjaList.Add(refundacja);
                        }
                    }
                }
                insertToDb(lekiList, refundacjaList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static List<Lek> search(string query, string queryType, string filter)
        {
            Model1 model = new Model1();
            IQueryable<Lek> result = model.Lek;

            if (filter == "psychotrop")
            {
                result = result.Where(p => p.Psychotrop);
            }
            else if (filter == "szczepionka")
            {
                result = result.Where(p => p.Szczepionka);
            }
            else if (filter == "senior")
            {
                result = result.Where(p => p.Senior);
            }

            if (queryType == "nazwa")
            {
                result = result.Where(p => p.Nazwa.Contains(query));
            }
            else if (queryType == "nazwaInt")
            {
                result = result.Where(p => p.NazwaInt.Contains(query));
            }
            else if (queryType == "EAN")
            {
                result = result.Where(p => p.EAN.Contains(query));
            }
            else if (queryType == "BL7")
            {
                result = result.Where(p => p.BL7.Contains(query));
            }

            return result.ToList();
        }

        public static List<DrugView> searchViewList(string query, string queryType, string filter)
        {
            Model1 model = new Model1();
            IQueryable<Lek> result = model.Lek;

            if (filter == "psychotrop")
            {
                result = result.Where(p => p.Psychotrop);
            }
            else if (filter == "szczepionka")
            {
                result = result.Where(p => p.Szczepionka);
            }
            else if (filter == "senior")
            {
                result = result.Where(p => p.Senior);
            }

            if (queryType == "nazwa")
            {
                result = result.Where(p => p.Nazwa.Contains(query));
            }
            else if (queryType == "nazwaInt")
            {
                result = result.Where(p => p.NazwaInt.Contains(query));
            }
            else if (queryType == "EAN")
            {
                result = result.Where(p => p.EAN.Contains(query));
            }
            else if (queryType == "BL7")
            {
                result = result.Where(p => p.BL7.Contains(query));
            }

            List<DrugView> searchResult = new List<DrugView>();
            foreach (Lek drug in result)
            {
                searchResult.Add(new DrugView(drug));
            }

            return searchResult;
        }

        public static List<DrugView> getViewList()
        {
            Model1 model = new Model1();
            var items = model.Lek.Where(p => p.Active).ToList();
            List<DrugView> result = new List<DrugView>();
            foreach (Lek drug in items)
            {
                result.Add(new DrugView(drug));
            }
            return result;
        }

        public static List<Lek> getAll()
        {
            Model1 model = new Model1();
            return model.Lek.ToList();
        }

        public static Lek get(string bl7)
        {
            Model1 model = new Model1();
            return model.Lek.Single(p => p.BL7 == bl7);
        }

        public static List<string> getSearchQueryTypes()
        {
            return new List<string> { "nazwa", "nazwaInt", "EAN", "BL7" };
        }

        public static List<string> getSearchFilters()
        {
            return new List<string> { "wszystko", "psychotrop", "szczepionka", "senior" };
        }

        public static int countElements(string path)
        {
            XDocument doc = XDocument.Load(path);
            int count = doc.Element("Leki").Elements("Lek").Count();
            return count;
        }
    }
}
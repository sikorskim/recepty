using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class Gabinet
    {
        public int GabinetId { get; private set; }

        public string Nazwa { get; private set; }

        public string REGON { get; private set; }

        public string NIP { get; private set; }

        public Address Adres { get; private set; }

        Gabinet()
        { }
        public Gabinet(string nazwa, string regon, string nip, Address adres)
        {
            Nazwa = nazwa;
            REGON = regon;
            NIP = nip;
            Adres = adres;
        }

        public bool insertToDb()
        {
            try
            {
                var db = new Model1();
                db.Gabinet.Add(this);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static Gabinet get()
        {
            Model1 model = new Model1();
            Gabinet gabinet = model.Gabinet.FirstOrDefault();
           // gabinet.Adres = model.Address.Single(p => p.AddressId == Adres.AddressId);
            return gabinet;
        }
    }
}
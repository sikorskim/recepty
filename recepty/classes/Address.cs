using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class Address
    {
        public int AddressId { get; private set; }
        public string Street { get; private set; }
        public string BuldingNumber { get; private set; }
        public string LocalNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public virtual string FullAddress {get{ return Street + " " + BuldingNumber + " " + LocalNumber + "\n" + PostalCode + " " + City; } }

        public Address()
        { }

        public Address(string street, string buldingNumber, string localNumber, string postalCode, string city)
        {
            Street = street;
            BuldingNumber = buldingNumber;
            LocalNumber = localNumber;
            PostalCode = postalCode;
            City = city;
        }

        public bool insertToDb()
        {
            try
            {
                var db = new Model1();
                db.Address.Add(this);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool edit(Address adres)
        {
            try
            {
                Model1 model = new Model1();
                Street = adres.Street;
                BuldingNumber = adres.BuldingNumber;
                LocalNumber = adres.LocalNumber;
                PostalCode = adres.PostalCode;
                City = adres.City;

                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static bool remove(int adresId)
        {
            try
            {
                Model1 model = new Model1();
                Address adres = model.Address.Single(p => p.AddressId == adresId);
                model.Address.Remove(adres);
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static Address get(int addressId)
        {
            Model1 model = new Model1();
            return model.Address.Single(p => p.AddressId == addressId);
        }
    }
}

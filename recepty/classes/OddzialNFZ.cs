using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class OddzialNFZ
    {
        [Key]
        public string Kod { get; private set; }
        public string Nazwa { get; private set; }

        public OddzialNFZ(string kod, string nazwa)
        {
            Kod = kod;
            Nazwa = nazwa;
        }
        public OddzialNFZ()
        { }

        public static OddzialNFZ getNazwa(string kod)
        {
            try
            {
                var db = new Model1();
                return db.NFZDepartament.Where(p => p.Kod == kod).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static List<OddzialNFZ> getOddzialy()
        {
            try
            {
                var db = new Model1();
                return db.NFZDepartament.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}

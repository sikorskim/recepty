using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
   public class Refundacja
{
        public int RefundacjaId { get; private set; }
        public string Poziom { get; private set; }
        public string Tekst { get; private set; }
        public Lek Lek { get; private set; }

        Refundacja()
        { }

        public Refundacja(string poziom, string tekst, Lek lek)
        {
            Poziom = poziom;
            Tekst = tekst;
            Lek = lek;
        }

        public static List<Refundacja> getRefunds(string bl7)
        {
            List<Refundacja> result = new List<Refundacja>();
            Model1 model = new Model1();
            result = model.Refundacja.Where(p => p.Lek.BL7 == bl7).ToList();
            return result;
        }
    }
}

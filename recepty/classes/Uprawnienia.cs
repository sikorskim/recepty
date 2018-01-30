using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class Uprawnienie
    {
        public int UprawnienieId { get; private set; }
        public string Kod { get; private set; }

        public Uprawnienie()
        { }

        public Uprawnienie(string kod)
        {
            Kod = kod;
        }

        public static List<Uprawnienie> getAll()
        {
            Model1 model = new Model1();
            return model.Uprawnienie.ToList();
        }
    }
}

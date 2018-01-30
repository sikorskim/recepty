using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
   public class Diagnostics
    {
        public int DiagnosticsId { get; private set; }
        public DateTime Time { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public Diagnostics()
        { }

        public Diagnostics(string title, string content)
        {
            Title = title;
            Content = content;
            Time = DateTime.Now;
        }

        public static void insertToDb(Diagnostics diagnostics)
        {
            Model1 model = new Model1();
            model.Diagnostics.Add(diagnostics);
            model.SaveChanges();
        }

        public static List<Diagnostics> getAll()
        {
            Model1 model = new Model1();
            return model.Diagnostics.ToList();
        }
    }
}

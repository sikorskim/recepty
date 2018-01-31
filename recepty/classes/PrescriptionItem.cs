using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class PrescriptionItem
    {
        public int PrescriptionItemId { get; private set; }
        public int PrescriptionId { get; private set; }
        [ForeignKey("PrescriptionId")]
        public virtual Prescription Prescription { get; set; }
        public string BL7 { get; private set; }
        [ForeignKey("BL7")]
        public virtual Lek Lek { get; private set; }

        public PrescriptionItem()
        { }

        public PrescriptionItem(Prescription prescription, Lek lek)
        {
            PrescriptionId = prescription.PrescriptionId;
            BL7 = lek.BL7;
        }

        public static List<DrugView> get(int prescriptionId)
        {
            Model1 model = new Model1();
            List<DrugView> result = new List<DrugView>();
            var items = model.PrescriptionItem.Where(p => p.PrescriptionId == prescriptionId);
            foreach (PrescriptionItem pi in items)
            {
                result.Add(new DrugView(pi.Lek));
            }

            return result;
        }
    }
}

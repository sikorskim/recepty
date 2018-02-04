using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace recepty
{
    public class PrescriptionNumber
    {
        public int PrescriptionNumberId { get; private set; }
        public string Number { get; private set; }
        public bool Used { get; private set; }
        public PulaRecept PrescriptionList { get; private set; }

        public PrescriptionNumber() { }

        public PrescriptionNumber(string number, PulaRecept prescriptionList)
        {
            Number = number;
            PrescriptionList = prescriptionList;
            Used = false;
        }

        public bool insertToDb()
        {
            try
            {
                Model1 model = new Model1();
                model.PrescriptionNumber.Add(this);
                model.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public static List<PrescriptionNumber> getAll()
        {
            Model1 model = new Model1();
            return model.PrescriptionNumber.ToList();
        }

        public static PrescriptionNumber getUnusedNumber(string category)
        {
            try
            {
                Model1 model = new Model1();
               PrescriptionNumber prescriptionNumber= model.PrescriptionNumber.Where(p => p.Used == false && p.PrescriptionList.Kategoria == category).FirstOrDefault();
                setNumberUsed(prescriptionNumber);
                return prescriptionNumber;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool setNumberUsed(PrescriptionNumber prescriptionNumber)
        {
            try
            {
                Model1 model = new Model1();
                prescriptionNumber.Used = true;
                model.Entry(prescriptionNumber).State = EntityState.Modified;
                model.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Diagnostics.insertToDb(new Diagnostics("PrescriptionNumber.setNumberUsed()", e.ToString()));
                return false;
            }
        }
    }
}
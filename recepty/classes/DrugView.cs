﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recepty
{
    public class DrugView
    {
        public string BL7 { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Substancja czynna")]
        public string NameInt { get; set; }
        [DisplayName("Postać")]
        public string Form { get; set; }
        [DisplayName("Dawka")]
        public string Dose { get; set; }
        [DisplayName("Opakowanie")]
        public string Package { get; set; }

        public DrugView()
        { }

        public DrugView(Lek drug)
        {
            BL7 = drug.BL7;
            Name = drug.Nazwa;
            NameInt = drug.NazwaInt;
            Form = drug.Postac;
            Dose = drug.Dawka;
            Package = drug.Opakowanie;
        }

        public static Lek[] convertDrugViewToDrug(List<DrugView> drugViewList)
        {
            Lek[] drugList = new Lek[5];
            Model1 model = new Model1();

            int i = 0;
            foreach (DrugView drugView in drugViewList)
            {
                drugList[i]=model.Lek.Single(p => p.BL7 == drugView.BL7);
                i++;
            }


            return drugList;
        }
    }
}

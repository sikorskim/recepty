namespace recepty.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<recepty.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(recepty.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            OddzialNFZ nfz01 = new OddzialNFZ("01", "Dolnoslaski");
            OddzialNFZ nfz02 = new OddzialNFZ("02", "Kujawsko-Pomorski");
            OddzialNFZ nfz03 = new OddzialNFZ("03", "Lubelski");
            OddzialNFZ nfz04 = new OddzialNFZ("04", "Lubuski");
            OddzialNFZ nfz05 = new OddzialNFZ("05", "Lodzki");
            OddzialNFZ nfz06 = new OddzialNFZ("06", "Malopolski");
            OddzialNFZ nfz07 = new OddzialNFZ("07", "Mazowiecki");
            OddzialNFZ nfz08 = new OddzialNFZ("08", "Opolski");
            OddzialNFZ nfz09 = new OddzialNFZ("09", "Podkarpacki");
            OddzialNFZ nfz10 = new OddzialNFZ("10", "Podlaski");
            OddzialNFZ nfz11 = new OddzialNFZ("11", "Pomorski");
            OddzialNFZ nfz12 = new OddzialNFZ("12", "Slaski");
            OddzialNFZ nfz13 = new OddzialNFZ("13", "Swietokrzyski");
            OddzialNFZ nfz14 = new OddzialNFZ("14", "Warminsko-Mazurski");
            OddzialNFZ nfz15 = new OddzialNFZ("15", "Wielkopolski");
            OddzialNFZ nfz16 = new OddzialNFZ("16", "Zachodniopomorski");

            List<OddzialNFZ> list = new List<OddzialNFZ>() {
                nfz01,nfz02,nfz03,nfz04,nfz05,nfz06,nfz07,nfz08,nfz09,nfz10,nfz11,nfz12,nfz13,nfz14,nfz15,nfz16
            };

            foreach (OddzialNFZ oddzial in list)
            {
                context.NFZDepartament.AddOrUpdate(oddzial);
            }

            Uprawnienie upr0 = new Uprawnienie("X");
            Uprawnienie upr1 = new Uprawnienie("S");
            Uprawnienie upr2 = new Uprawnienie("IB");
            Uprawnienie upr3 = new Uprawnienie("IW");
            Uprawnienie upr4 = new Uprawnienie("ZK");
            Uprawnienie upr5 = new Uprawnienie("AZ");
            Uprawnienie upr6 = new Uprawnienie("WP");
            Uprawnienie upr7 = new Uprawnienie("PO");
            Uprawnienie upr8 = new Uprawnienie("CN");
            Uprawnienie upr9 = new Uprawnienie("DN");
            Uprawnienie upr10 = new Uprawnienie("IN");

            List<Uprawnienie> uprawnienieList = new List<Uprawnienie> { upr0, upr1, upr2, upr3, upr4, upr5, upr6, upr7, upr8, upr9, upr10 };

            foreach (Uprawnienie upr in uprawnienieList)
            {
                context.Uprawnienie.AddOrUpdate(upr);
            }

            Doctor doctor = new Doctor("Lekarski", "Janusz", "90909898988", "012345", "lekarskij", "password");
            context.Doctor.AddOrUpdate(doctor);
        }
    }
}

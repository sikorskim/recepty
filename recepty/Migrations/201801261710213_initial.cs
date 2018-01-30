namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        BuldingNumber = c.String(),
                        LocalNumber = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Diagnostics",
                c => new
                    {
                        DiagnosticsId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.DiagnosticsId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Lastname = c.String(),
                        Name = c.String(),
                        PESEL = c.String(),
                        RightToPracticeNumber = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.Gabinets",
                c => new
                    {
                        GabinetId = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        REGON = c.String(),
                        NIP = c.String(),
                        Adres_AddressId = c.Int(),
                    })
                .PrimaryKey(t => t.GabinetId)
                .ForeignKey("dbo.Addresses", t => t.Adres_AddressId)
                .Index(t => t.Adres_AddressId);
            
            CreateTable(
                "dbo.Leks",
                c => new
                    {
                        BL7 = c.String(nullable: false, maxLength: 128),
                        EAN = c.String(),
                        Psychotrop = c.Boolean(nullable: false),
                        Senior = c.Boolean(nullable: false),
                        Szczepionka = c.Boolean(nullable: false),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nazwa = c.String(),
                        NazwaInt = c.String(),
                        Postac = c.String(),
                        Dawka = c.String(),
                        Opakowanie = c.String(),
                    })
                .PrimaryKey(t => t.BL7);
            
            CreateTable(
                "dbo.OddzialNFZs",
                c => new
                    {
                        Kod = c.String(nullable: false, maxLength: 128),
                        Nazwa = c.String(),
                    })
                .PrimaryKey(t => t.Kod);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        Lastname = c.String(),
                        Name = c.String(),
                        PESEL = c.String(),
                        AddressId = c.Int(nullable: false),
                        Kod = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.OddzialNFZs", t => t.Kod)
                .Index(t => t.AddressId)
                .Index(t => t.Kod);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        DateOfIssue = c.DateTime(nullable: false),
                        PrescriptionNumberId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrescriptionId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .ForeignKey("dbo.PrescriptionNumbers", t => t.PrescriptionNumberId, cascadeDelete: true)
                .Index(t => t.PrescriptionNumberId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.PrescriptionNumbers",
                c => new
                    {
                        PrescriptionNumberId = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        Used = c.Boolean(nullable: false),
                        PrescriptionList_PulaReceptId = c.Int(),
                    })
                .PrimaryKey(t => t.PrescriptionNumberId)
                .ForeignKey("dbo.PulaRecepts", t => t.PrescriptionList_PulaReceptId)
                .Index(t => t.PrescriptionList_PulaReceptId);
            
            CreateTable(
                "dbo.PulaRecepts",
                c => new
                    {
                        PulaReceptId = c.Int(nullable: false, identity: true),
                        Typ = c.String(),
                        Kategoria = c.String(),
                        NrPrawa = c.String(),
                        PESEL = c.String(),
                        ImieNazwisko = c.String(),
                        IloscRecept = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PulaReceptId);
            
            CreateTable(
                "dbo.Refundacjas",
                c => new
                    {
                        RefundacjaId = c.Int(nullable: false, identity: true),
                        Poziom = c.String(),
                        Tekst = c.String(),
                        Lek_BL7 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RefundacjaId)
                .ForeignKey("dbo.Leks", t => t.Lek_BL7)
                .Index(t => t.Lek_BL7);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Refundacjas", "Lek_BL7", "dbo.Leks");
            DropForeignKey("dbo.Prescriptions", "PrescriptionNumberId", "dbo.PrescriptionNumbers");
            DropForeignKey("dbo.PrescriptionNumbers", "PrescriptionList_PulaReceptId", "dbo.PulaRecepts");
            DropForeignKey("dbo.Prescriptions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Patients", "Kod", "dbo.OddzialNFZs");
            DropForeignKey("dbo.Patients", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Gabinets", "Adres_AddressId", "dbo.Addresses");
            DropIndex("dbo.Refundacjas", new[] { "Lek_BL7" });
            DropIndex("dbo.PrescriptionNumbers", new[] { "PrescriptionList_PulaReceptId" });
            DropIndex("dbo.Prescriptions", new[] { "DoctorId" });
            DropIndex("dbo.Prescriptions", new[] { "PatientId" });
            DropIndex("dbo.Prescriptions", new[] { "PrescriptionNumberId" });
            DropIndex("dbo.Patients", new[] { "Kod" });
            DropIndex("dbo.Patients", new[] { "AddressId" });
            DropIndex("dbo.Gabinets", new[] { "Adres_AddressId" });
            DropTable("dbo.Refundacjas");
            DropTable("dbo.PulaRecepts");
            DropTable("dbo.PrescriptionNumbers");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Patients");
            DropTable("dbo.OddzialNFZs");
            DropTable("dbo.Leks");
            DropTable("dbo.Gabinets");
            DropTable("dbo.Doctors");
            DropTable("dbo.Diagnostics");
            DropTable("dbo.Addresses");
        }
    }
}

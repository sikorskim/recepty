namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescriptionItems",
                c => new
                    {
                        PrescriptionItemId = c.Int(nullable: false, identity: true),
                        PrescriptionId = c.Int(nullable: false),
                        BL7 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PrescriptionItemId)
                .ForeignKey("dbo.Leks", t => t.BL7)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionId, cascadeDelete: true)
                .Index(t => t.PrescriptionId)
                .Index(t => t.BL7);
            
            CreateTable(
                "dbo.Uprawnienies",
                c => new
                    {
                        UprawnienieId = c.Int(nullable: false, identity: true),
                        Kod = c.String(),
                    })
                .PrimaryKey(t => t.UprawnienieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrescriptionItems", "PrescriptionId", "dbo.Prescriptions");
            DropForeignKey("dbo.PrescriptionItems", "BL7", "dbo.Leks");
            DropIndex("dbo.PrescriptionItems", new[] { "BL7" });
            DropIndex("dbo.PrescriptionItems", new[] { "PrescriptionId" });
            DropTable("dbo.Uprawnienies");
            DropTable("dbo.PrescriptionItems");
        }
    }
}

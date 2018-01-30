namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patients", "UprawnienieId", "dbo.Uprawnienies");
            DropIndex("dbo.Patients", new[] { "UprawnienieId" });
            AddColumn("dbo.Patients", "Uprawnienie", c => c.String());
            DropColumn("dbo.Patients", "UprawnienieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Patients", "UprawnienieId", c => c.Int(nullable: false));
            DropColumn("dbo.Patients", "Uprawnienie");
            CreateIndex("dbo.Patients", "UprawnienieId");
            AddForeignKey("dbo.Patients", "UprawnienieId", "dbo.Uprawnienies", "UprawnienieId", cascadeDelete: true);
        }
    }
}

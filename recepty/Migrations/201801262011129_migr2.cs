namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "UprawnienieId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patients", "UprawnienieId");
            AddForeignKey("dbo.Patients", "UprawnienieId", "dbo.Uprawnienies", "UprawnienieId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "UprawnienieId", "dbo.Uprawnienies");
            DropIndex("dbo.Patients", new[] { "UprawnienieId" });
            DropColumn("dbo.Patients", "UprawnienieId");
        }
    }
}

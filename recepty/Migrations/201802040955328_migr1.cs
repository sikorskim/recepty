namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PrescriptionItems", "RefundacjaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrescriptionItems", "RefundacjaId", c => c.Int(nullable: false));
        }
    }
}

namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr0 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrescriptionItems", "RefundacjaId", "dbo.Refundacjas");
            DropIndex("dbo.PrescriptionItems", new[] { "RefundacjaId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.PrescriptionItems", "RefundacjaId");
            AddForeignKey("dbo.PrescriptionItems", "RefundacjaId", "dbo.Refundacjas", "RefundacjaId", cascadeDelete: true);
        }
    }
}

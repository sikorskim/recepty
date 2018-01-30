namespace recepty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr0 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leks", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leks", "Active");
        }
    }
}

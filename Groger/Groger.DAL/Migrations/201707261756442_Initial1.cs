namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groceries", "Name", c => c.String());
            AlterColumn("dbo.Groceries", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groceries", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Groceries", "Name", c => c.String(nullable: false));
        }
    }
}

namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groceries", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groceries", "Quantity");
        }
    }
}

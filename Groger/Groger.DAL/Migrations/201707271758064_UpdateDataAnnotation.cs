namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Groceries", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Groceries", "Description", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Groceries", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groceries", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Groceries", "Description", c => c.String());
            AlterColumn("dbo.Groceries", "Name", c => c.String());
        }
    }
}

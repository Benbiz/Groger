namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UIntNotSupportedByEF : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClusterGroceries", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClusterGroceries", "Quantity");
        }
    }
}

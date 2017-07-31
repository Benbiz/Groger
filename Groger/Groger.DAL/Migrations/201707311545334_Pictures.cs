namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pictures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groceries", "Picture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groceries", "Picture");
        }
    }
}

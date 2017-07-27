namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataAnnotation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clusters", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Clusters", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clusters", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Clusters", "Name", c => c.String(nullable: false));
        }
    }
}

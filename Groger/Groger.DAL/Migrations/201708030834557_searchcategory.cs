namespace Groger.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class searchcategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.Categories", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "Name" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
    }
}

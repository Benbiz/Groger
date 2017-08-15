namespace Groger.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class OptionalCategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groceries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Groceries", new[] { "CategoryId" });
            AlterColumn("dbo.Groceries", "CategoryId", c => c.Int());
            CreateIndex("dbo.Groceries", "CategoryId");
            AddForeignKey("dbo.Groceries", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groceries", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Groceries", new[] { "CategoryId" });
            AlterColumn("dbo.Groceries", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Groceries", "CategoryId");
            AddForeignKey("dbo.Groceries", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}

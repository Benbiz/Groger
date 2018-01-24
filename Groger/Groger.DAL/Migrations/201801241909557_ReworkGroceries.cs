namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkGroceries : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groceries", "ClusterId", "dbo.Clusters");
            DropIndex("dbo.Groceries", new[] { "ClusterId" });
            CreateTable(
                "dbo.ClusterGroceries",
                c => new
                    {
                        ClusterId = c.Int(nullable: false),
                        GroceryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClusterId, t.GroceryId })
                .ForeignKey("dbo.Clusters", t => t.ClusterId, cascadeDelete: true)
                .ForeignKey("dbo.Groceries", t => t.GroceryId, cascadeDelete: true)
                .Index(t => t.ClusterId)
                .Index(t => t.GroceryId);
            
            DropColumn("dbo.Groceries", "Quantity");
            DropColumn("dbo.Groceries", "ClusterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groceries", "ClusterId", c => c.Int(nullable: false));
            AddColumn("dbo.Groceries", "Quantity", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClusterGroceries", "GroceryId", "dbo.Groceries");
            DropForeignKey("dbo.ClusterGroceries", "ClusterId", "dbo.Clusters");
            DropIndex("dbo.ClusterGroceries", new[] { "GroceryId" });
            DropIndex("dbo.ClusterGroceries", new[] { "ClusterId" });
            DropTable("dbo.ClusterGroceries");
            CreateIndex("dbo.Groceries", "ClusterId");
            AddForeignKey("dbo.Groceries", "ClusterId", "dbo.Clusters", "Id", cascadeDelete: true);
        }
    }
}

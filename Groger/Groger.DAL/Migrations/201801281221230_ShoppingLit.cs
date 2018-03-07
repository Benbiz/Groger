namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoppingLit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Comment = c.String(),
                        Cluster_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clusters", t => t.Cluster_Id)
                .Index(t => t.Cluster_Id);
            
            CreateTable(
                "dbo.ShoppingItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClusterGroceryId = c.Int(nullable: false),
                        ToBuy = c.Int(nullable: false),
                        Brought = c.Int(nullable: false),
                        Validated = c.Boolean(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        ValidatedDate = c.DateTime(nullable: false),
                        Grocery_ClusterId = c.Int(nullable: false),
                        Grocery_GroceryId = c.Int(nullable: false),
                        ShoppingList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClusterGroceries", t => new { t.Grocery_ClusterId, t.Grocery_GroceryId }, cascadeDelete: true)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_Id)
                .Index(t => new { t.Grocery_ClusterId, t.Grocery_GroceryId })
                .Index(t => t.ShoppingList_Id);
            
            AddColumn("dbo.ClusterGroceries", "Unit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingItems", "ShoppingList_Id", "dbo.ShoppingLists");
            DropForeignKey("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" }, "dbo.ClusterGroceries");
            DropForeignKey("dbo.ShoppingLists", "Cluster_Id", "dbo.Clusters");
            DropIndex("dbo.ShoppingItems", new[] { "ShoppingList_Id" });
            DropIndex("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" });
            DropIndex("dbo.ShoppingLists", new[] { "Cluster_Id" });
            DropColumn("dbo.ClusterGroceries", "Unit");
            DropTable("dbo.ShoppingItems");
            DropTable("dbo.ShoppingLists");
        }
    }
}

namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroceryInShoppingItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" }, "dbo.ClusterGroceries");
            DropIndex("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" });
            RenameColumn(table: "dbo.ShoppingItems", name: "Grocery_ClusterId", newName: "GroceryId");
            AddColumn("dbo.ShoppingItems", "Comment", c => c.String());
            CreateIndex("dbo.ShoppingItems", "GroceryId");
            AddForeignKey("dbo.ShoppingItems", "GroceryId", "dbo.Groceries", "Id", cascadeDelete: true);
            DropColumn("dbo.ClusterGroceries", "Unit");
            DropColumn("dbo.ShoppingItems", "ClusterGroceryId");
            DropColumn("dbo.ShoppingItems", "Grocery_GroceryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingItems", "Grocery_GroceryId", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingItems", "ClusterGroceryId", c => c.Int(nullable: false));
            AddColumn("dbo.ClusterGroceries", "Unit", c => c.Int(nullable: false));
            DropForeignKey("dbo.ShoppingItems", "GroceryId", "dbo.Groceries");
            DropIndex("dbo.ShoppingItems", new[] { "GroceryId" });
            DropColumn("dbo.ShoppingItems", "Comment");
            RenameColumn(table: "dbo.ShoppingItems", name: "GroceryId", newName: "Grocery_ClusterId");
            CreateIndex("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" });
            AddForeignKey("dbo.ShoppingItems", new[] { "Grocery_ClusterId", "Grocery_GroceryId" }, "dbo.ClusterGroceries", new[] { "ClusterId", "GroceryId" }, cascadeDelete: true);
        }
    }
}

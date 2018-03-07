namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateShopping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingLists", "Cluster_Id", "dbo.Clusters");
            DropIndex("dbo.ShoppingLists", new[] { "Cluster_Id" });
            AddColumn("dbo.ShoppingLists", "Validated", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShoppingLists", "ValidatedDate", c => c.DateTime());
            AlterColumn("dbo.ShoppingLists", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ShoppingLists", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ShoppingLists", "Cluster_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ShoppingItems", "ValidatedDate", c => c.DateTime());
            CreateIndex("dbo.ShoppingLists", "Cluster_Id");
            AddForeignKey("dbo.ShoppingLists", "Cluster_Id", "dbo.Clusters", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingLists", "Cluster_Id", "dbo.Clusters");
            DropIndex("dbo.ShoppingLists", new[] { "Cluster_Id" });
            AlterColumn("dbo.ShoppingItems", "ValidatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ShoppingLists", "Cluster_Id", c => c.Int());
            AlterColumn("dbo.ShoppingLists", "Description", c => c.String());
            AlterColumn("dbo.ShoppingLists", "Name", c => c.String());
            DropColumn("dbo.ShoppingLists", "ValidatedDate");
            DropColumn("dbo.ShoppingLists", "Validated");
            CreateIndex("dbo.ShoppingLists", "Cluster_Id");
            AddForeignKey("dbo.ShoppingLists", "Cluster_Id", "dbo.Clusters", "Id");
        }
    }
}

namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentToDescriptionShoppingList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "Description", c => c.String());
            AddColumn("dbo.ShoppingLists", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ClusterGroceries", "Id");
            DropColumn("dbo.ShoppingLists", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingLists", "Comment", c => c.String());
            AddColumn("dbo.ClusterGroceries", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.ShoppingLists", "CreateDate");
            DropColumn("dbo.ShoppingLists", "Description");
        }
    }
}

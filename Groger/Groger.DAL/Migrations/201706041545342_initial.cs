namespace Groger.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clusters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groceries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ClusterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clusters", t => t.ClusterId, cascadeDelete: true)
                .Index(t => t.ClusterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Groceries", "ClusterId", "dbo.Clusters");
            DropIndex("dbo.Groceries", new[] { "ClusterId" });
            DropTable("dbo.Groceries");
            DropTable("dbo.Clusters");
        }
    }
}

namespace JuicesMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        IsNotSpecified = c.Boolean(nullable: false),
                        Chemical_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chemicals", t => t.Chemical_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Chemical_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Technologies", "Product_Id", c => c.Int());
            CreateIndex("dbo.Technologies", "Product_Id");
            AddForeignKey("dbo.Technologies", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Technologies", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Contents", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Contents", "Chemical_Id", "dbo.Chemicals");
            DropIndex("dbo.Technologies", new[] { "Product_Id" });
            DropIndex("dbo.Contents", new[] { "Product_Id" });
            DropIndex("dbo.Contents", new[] { "Chemical_Id" });
            DropColumn("dbo.Technologies", "Product_Id");
            DropTable("dbo.Contents");
        }
    }
}

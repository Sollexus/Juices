namespace JuicesMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lolzies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contents", "Chemical_Id", "dbo.Chemicals");
            DropIndex("dbo.Contents", new[] { "Chemical_Id" });
            AlterColumn("dbo.Contents", "Chemical_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Contents", "Chemical_Id");
            AddForeignKey("dbo.Contents", "Chemical_Id", "dbo.Chemicals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "Chemical_Id", "dbo.Chemicals");
            DropIndex("dbo.Contents", new[] { "Chemical_Id" });
            AlterColumn("dbo.Contents", "Chemical_Id", c => c.Int());
            CreateIndex("dbo.Contents", "Chemical_Id");
            AddForeignKey("dbo.Contents", "Chemical_Id", "dbo.Chemicals", "Id");
        }
    }
}

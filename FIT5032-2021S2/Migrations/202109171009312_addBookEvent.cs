namespace FIT5032_2021S2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBookEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookEvents",
                c => new
                    {
                        StoreEventId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.StoreEventId, t.UserId })
                .ForeignKey("dbo.StoreEvents", t => t.StoreEventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.StoreEventId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookEvents", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookEvents", "StoreEventId", "dbo.StoreEvents");
            DropIndex("dbo.BookEvents", new[] { "UserId" });
            DropIndex("dbo.BookEvents", new[] { "StoreEventId" });
            DropTable("dbo.BookEvents");
        }
    }
}

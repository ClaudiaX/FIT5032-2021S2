namespace FIT5032_2021S2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEventColor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventTypes", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventTypes", "Color");
        }
    }
}

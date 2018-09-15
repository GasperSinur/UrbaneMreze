namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entities", "PinGuid", "dbo.Pins");
            DropIndex("dbo.Entities", new[] { "PinGuid" });
            DropColumn("dbo.Entities", "PinGuid");
        }
        
        public override void Down()
        {
            
        }
    }
}

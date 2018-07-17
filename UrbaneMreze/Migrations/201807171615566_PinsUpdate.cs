namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PinsUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pins",
                c => new
                    {
                        PinGuid = c.Guid(nullable: false),
                        Name = c.String(),
                        Icon = c.Binary(),
                        Color = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PinGuid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pins");
        }
    }
}

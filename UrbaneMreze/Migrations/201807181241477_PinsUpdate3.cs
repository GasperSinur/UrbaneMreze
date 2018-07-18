namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PinsUpdate3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Pins");

            CreateTable(
                "dbo.Pins",
                c => new
                    {
                        PinGuid = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Icon = c.Binary(nullable: false),
                        Color = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserCreatedID = c.String(),
                        UserModifiedID = c.String(),
                    })
                .PrimaryKey(t => t.PinGuid);
            
            
        }
        
        public override void Down()
        {
        }
    }
}

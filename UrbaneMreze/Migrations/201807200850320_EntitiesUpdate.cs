namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityGuid = c.Guid(nullable: false),
                        TypeGuid = c.Guid(nullable: false),
                        EntityName = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserCreatedID = c.String(),
                        UserModifiedID = c.String(),
                    })
                .PrimaryKey(t => t.EntityGuid)
                .ForeignKey("dbo.Types", t => t.TypeGuid, cascadeDelete: true)
                .Index(t => t.TypeGuid);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeGuid = c.Guid(nullable: false),
                        TypeName = c.String(nullable: false),
                        Description = c.String(),
                        PinGuid = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserCreatedID = c.String(),
                        UserModifiedID = c.String(),
                    })
                .PrimaryKey(t => t.TypeGuid)
                .ForeignKey("dbo.Pins", t => t.PinGuid, cascadeDelete: true)
                .Index(t => t.PinGuid);
            
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
            DropForeignKey("dbo.Entities", "TypeGuid", "dbo.Types");
            DropForeignKey("dbo.Types", "PinGuid", "dbo.Pins");
            DropIndex("dbo.Types", new[] { "PinGuid" });
            DropIndex("dbo.Entities", new[] { "TypeGuid" });
            DropTable("dbo.Pins");
            DropTable("dbo.Types");
            DropTable("dbo.Entities");
        }
    }
}

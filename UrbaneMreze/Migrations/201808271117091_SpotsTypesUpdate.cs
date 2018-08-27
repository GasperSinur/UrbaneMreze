namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotsTypesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Types", "PinGuid", "dbo.Pins");
            DropForeignKey("dbo.Entities", "TypeGuid", "dbo.Types");
            DropForeignKey("dbo.SpotEntities", "EntityGuid", "dbo.Entities");
            DropForeignKey("dbo.SpotEntities", "SpotGuid", "dbo.Spots");
            DropIndex("dbo.Entities", new[] { "TypeGuid" });
            DropIndex("dbo.Types", new[] { "PinGuid" });
            DropIndex("dbo.SpotEntities", new[] { "SpotGuid" });
            DropIndex("dbo.SpotEntities", new[] { "EntityGuid" });
            CreateTable(
                "dbo.SpotTypes",
                c => new
                    {
                        SpotTypeGuid = c.Guid(nullable: false),
                        SpotGuid = c.Guid(nullable: false),
                        TypeGuid = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpotTypeGuid)
                .ForeignKey("dbo.Spots", t => t.SpotGuid, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeGuid, cascadeDelete: true)
                .Index(t => t.SpotGuid)
                .Index(t => t.TypeGuid);
            
            AddColumn("dbo.Entities", "PinGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.Types", "EntityGuid", c => c.Guid(nullable: false));
            CreateIndex("dbo.Types", "EntityGuid");
            CreateIndex("dbo.Entities", "PinGuid");
            AddForeignKey("dbo.Entities", "PinGuid", "dbo.Pins", "PinGuid", cascadeDelete: true);
            AddForeignKey("dbo.Types", "EntityGuid", "dbo.Entities", "EntityGuid", cascadeDelete: true);
            DropColumn("dbo.Entities", "TypeGuid");
            DropColumn("dbo.Types", "PinGuid");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SpotEntities",
                c => new
                    {
                        SpotEntityGuid = c.Guid(nullable: false),
                        SpotGuid = c.Guid(nullable: false),
                        EntityGuid = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SpotEntityGuid);
            
            AddColumn("dbo.Types", "PinGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.Entities", "TypeGuid", c => c.Guid(nullable: false));
            DropForeignKey("dbo.SpotTypes", "TypeGuid", "dbo.Types");
            DropForeignKey("dbo.Types", "EntityGuid", "dbo.Entities");
            DropForeignKey("dbo.Entities", "PinGuid", "dbo.Pins");
            DropForeignKey("dbo.SpotTypes", "SpotGuid", "dbo.Spots");
            DropIndex("dbo.Entities", new[] { "PinGuid" });
            DropIndex("dbo.Types", new[] { "EntityGuid" });
            DropIndex("dbo.SpotTypes", new[] { "TypeGuid" });
            DropIndex("dbo.SpotTypes", new[] { "SpotGuid" });
            DropColumn("dbo.Types", "EntityGuid");
            DropColumn("dbo.Entities", "PinGuid");
            DropTable("dbo.SpotTypes");
            CreateIndex("dbo.SpotEntities", "EntityGuid");
            CreateIndex("dbo.SpotEntities", "SpotGuid");
            CreateIndex("dbo.Types", "PinGuid");
            CreateIndex("dbo.Entities", "TypeGuid");
            AddForeignKey("dbo.SpotEntities", "SpotGuid", "dbo.Spots", "SpotGuid", cascadeDelete: true);
            AddForeignKey("dbo.SpotEntities", "EntityGuid", "dbo.Entities", "EntityGuid", cascadeDelete: true);
            AddForeignKey("dbo.Entities", "TypeGuid", "dbo.Types", "TypeGuid", cascadeDelete: true);
            AddForeignKey("dbo.Types", "PinGuid", "dbo.Pins", "PinGuid", cascadeDelete: true);
        }
    }
}

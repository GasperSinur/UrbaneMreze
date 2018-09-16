namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotTypesUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpotsTypes", "TypeGuid", "dbo.Types");
            DropForeignKey("dbo.SpotsTypes", "SpotGuid", "dbo.Spots");
            DropIndex("dbo.SpotsTypes", new[] { "TypeGuid" });
            DropIndex("dbo.SpotsTypes", new[] { "SpotGuid" });
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
        }
        
        public override void Down()
        {
        }
    }
}

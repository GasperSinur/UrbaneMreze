namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotsEntititesUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpotTypes", "TypeGuid", "dbo.Types");
            DropForeignKey("dbo.SpotTypes", "SpotGuid", "dbo.Spots");
            DropIndex("dbo.SpotTypes", new[] { "TypeGuid" });
            DropIndex("dbo.SpotTypes", new[] { "SpotGuid" });

            CreateTable(
                "dbo.SpotsTypes",
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

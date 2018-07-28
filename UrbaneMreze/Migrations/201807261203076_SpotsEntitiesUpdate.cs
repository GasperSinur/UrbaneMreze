namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotsEntitiesUpdate : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.SpotEntityGuid)
                .ForeignKey("dbo.Entities", t => t.EntityGuid, cascadeDelete: true)
                .ForeignKey("dbo.Spots", t => t.SpotGuid, cascadeDelete: true)
                .Index(t => t.SpotGuid)
                .Index(t => t.EntityGuid);
        }
        
        public override void Down()
        {
        }
    }
}

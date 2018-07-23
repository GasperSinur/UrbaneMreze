namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotosUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                {
                    PhotoGuid = c.Guid(nullable: false),
                    SpotGuid = c.Guid(nullable: false),
                    Description = c.String(nullable: true),
                    Longitude = c.Double(nullable: false),
                    Latitude = c.Double(nullable: false),
                    File = c.Binary(nullable: false),
                    Thumbnail = c.Binary(nullable: false),
                    ContentType = c.String(nullable: true),
                    DateCreated = c.DateTime(nullable: false),
                    DateModified = c.DateTime(nullable: false),
                    UserCreatedID = c.Guid(nullable: false),
                    UserModifiedID = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.PhotoGuid)
                .ForeignKey("dbo.Spots", t => t.SpotGuid, cascadeDelete: true)
                .Index(t => t.SpotGuid);
        }
        
        public override void Down()
        {
        }
    }
}

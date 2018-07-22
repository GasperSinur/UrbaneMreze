namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentsUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentGuid = c.Guid(nullable: false),
                        SpotGuid = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserCreatedID = c.Guid(nullable: false),
                        UserModifiedID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CommentGuid)
                .ForeignKey("dbo.Spots", t => t.SpotGuid, cascadeDelete: true)
                .Index(t => t.SpotGuid);
        }
        
        public override void Down()
        {
        }
    }
}

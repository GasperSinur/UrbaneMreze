namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesUpdate3 : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
               "dbo.Entities",
               c => new
               {
                   EntityGuid = c.Guid(nullable: false),
                   EntityName = c.String(nullable: false),
                   Description = c.String(nullable: true),
                   DateCreated = c.DateTime(nullable: false),
                   DateModified = c.DateTime(nullable: false),
                   UserCreatedID = c.Guid(nullable: false),
                   UserModifiedID = c.Guid(nullable: false),
               })
               .PrimaryKey(t => t.EntityGuid);

            AddForeignKey("dbo.Types", "EntityGuid", "dbo.Entities");
        }
        
        public override void Down()
        {
        }
    }
}

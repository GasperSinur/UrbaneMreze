namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypesUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SpotTypes", "TypeGuid", "dbo.Types");
            DropTable("dbo.Types");
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeGuid = c.Guid(nullable: false),
                        EntityGuid = c.Guid(nullable: false),
                        PinGuid = c.Guid(nullable: false),
                        TypeName = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        UserCreatedID = c.Guid(nullable: false),
                        UserModifiedID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.TypeGuid)
                .ForeignKey("dbo.Entities", t => t.EntityGuid, cascadeDelete: true)
                .ForeignKey("dbo.Pins", t => t.PinGuid, cascadeDelete: true)
                .Index(t => t.EntityGuid)
                .Index(t => t.PinGuid);
            AddForeignKey("dbo.SpotTypes", "TypeGuid", "dbo.Types", "TypeGuid", cascadeDelete: true);

        }
        
        public override void Down()
        {

        }
    }
}

namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Types", "EntityGuid", "dbo.Entities");
            DropTable("dbo.Entities");
        }
        
        public override void Down()
        {
            
        }
    }
}

namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotosUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photos", "ContentType");
        }
        
        public override void Down()
        {
        }
    }
}

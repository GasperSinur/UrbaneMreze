namespace UrbaneMreze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PinsUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pins", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Pins", "Icon", c => c.Binary(nullable: false));
            AlterColumn("dbo.Pins", "Color", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pins", "Color", c => c.String());
            AlterColumn("dbo.Pins", "Icon", c => c.Binary());
            AlterColumn("dbo.Pins", "Name", c => c.String());
        }
    }
}

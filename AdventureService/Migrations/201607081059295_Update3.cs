namespace AdventureService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventInfoes", "PlacesTaken", c => c.Int(nullable: false));
            DropColumn("dbo.EventInfoes", "Available");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventInfoes", "Available", c => c.Boolean(nullable: false));
            DropColumn("dbo.EventInfoes", "PlacesTaken");
        }
    }
}

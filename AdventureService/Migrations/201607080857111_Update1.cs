namespace AdventureService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdventureEvents", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdventureEvents", "MyProperty", c => c.Int(nullable: false));
        }
    }
}

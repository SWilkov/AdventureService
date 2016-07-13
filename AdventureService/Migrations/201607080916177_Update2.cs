namespace AdventureService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Gender", c => c.Int(nullable: false));
        }
    }
}

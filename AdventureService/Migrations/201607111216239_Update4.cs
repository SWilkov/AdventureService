namespace AdventureService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerEvents", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerEvents", "AdventureEventId", "dbo.AdventureEvents");
            DropIndex("dbo.CustomerEvents", new[] { "CustomerId" });
            DropIndex("dbo.CustomerEvents", new[] { "AdventureEventId" });
            AddColumn("dbo.AdventureEvents", "Customer_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "EventInfo_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AdventureEvents", "Customer_Id");
            CreateIndex("dbo.Customers", "EventInfo_Id");
            AddForeignKey("dbo.AdventureEvents", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Customers", "EventInfo_Id", "dbo.EventInfoes", "Id");
            DropTable("dbo.CustomerEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerEvents",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        AdventureEventId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.AdventureEventId });
            
            DropForeignKey("dbo.Customers", "EventInfo_Id", "dbo.EventInfoes");
            DropForeignKey("dbo.AdventureEvents", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "EventInfo_Id" });
            DropIndex("dbo.AdventureEvents", new[] { "Customer_Id" });
            DropColumn("dbo.Customers", "EventInfo_Id");
            DropColumn("dbo.AdventureEvents", "Customer_Id");
            CreateIndex("dbo.CustomerEvents", "AdventureEventId");
            CreateIndex("dbo.CustomerEvents", "CustomerId");
            AddForeignKey("dbo.CustomerEvents", "AdventureEventId", "dbo.AdventureEvents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerEvents", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}

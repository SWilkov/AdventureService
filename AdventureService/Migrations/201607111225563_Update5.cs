namespace AdventureService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdventureEvents", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "EventInfo_Id", "dbo.EventInfoes");
            DropIndex("dbo.AdventureEvents", new[] { "Customer_Id" });
            DropIndex("dbo.Customers", new[] { "EventInfo_Id" });
            CreateTable(
                "dbo.CustomerEvents",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        EventInfoId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.EventInfoId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.EventInfoes", t => t.EventInfoId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EventInfoId);
            
            DropColumn("dbo.AdventureEvents", "Customer_Id");
            DropColumn("dbo.Customers", "EventInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "EventInfo_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AdventureEvents", "Customer_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.CustomerEvents", "EventInfoId", "dbo.EventInfoes");
            DropForeignKey("dbo.CustomerEvents", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerEvents", new[] { "EventInfoId" });
            DropIndex("dbo.CustomerEvents", new[] { "CustomerId" });
            DropTable("dbo.CustomerEvents");
            CreateIndex("dbo.Customers", "EventInfo_Id");
            CreateIndex("dbo.AdventureEvents", "Customer_Id");
            AddForeignKey("dbo.Customers", "EventInfo_Id", "dbo.EventInfoes", "Id");
            AddForeignKey("dbo.AdventureEvents", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}

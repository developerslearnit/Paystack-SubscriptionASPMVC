namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        CartCode = c.String(nullable: false, maxLength: 50),
                        PlanName = c.String(nullable: false, maxLength: 150),
                        PlanId = c.String(nullable: false, maxLength: 50),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerCode = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        CustomerSince = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false, identity: true),
                        CustomerCode = c.String(nullable: false, maxLength: 10),
                        PlanName = c.String(nullable: false, maxLength: 150),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentRef = c.String(nullable: false, maxLength: 50),
                        Confirmed = c.Boolean(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriptionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
        }
    }
}

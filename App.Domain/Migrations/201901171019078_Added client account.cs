namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedclientaccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubscriberAccounts",
                c => new
                    {
                        SubscriberAccountId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        PlanCode = c.String(nullable: false, maxLength: 30),
                        SubscriptionCode = c.String(nullable: false, maxLength: 100),
                        EmailToken = c.String(nullable: false, maxLength: 100),
                        SubscriptionDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriberAccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SubscriberAccounts");
        }
    }
}

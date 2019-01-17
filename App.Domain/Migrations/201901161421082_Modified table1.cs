namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifiedtable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "PlanId", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "PlanId");
        }
    }
}

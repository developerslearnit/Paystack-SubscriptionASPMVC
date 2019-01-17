namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifiedtable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerSince", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerSince", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

namespace SmsModemClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class balance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmsModemBlocks", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmsModemBlocks", "Balance");
        }
    }
}

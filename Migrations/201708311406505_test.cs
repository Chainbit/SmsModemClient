namespace SmsModemClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SmsModemBlocks", "SignalLevel");
            DropColumn("dbo.SmsModemBlocks", "MacAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SmsModemBlocks", "MacAddress", c => c.String());
            AddColumn("dbo.SmsModemBlocks", "SignalLevel", c => c.Int(nullable: false));
        }
    }
}

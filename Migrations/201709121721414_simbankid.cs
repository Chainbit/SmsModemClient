namespace SmsModemClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class simbankid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SmsModemBlocks", "SimBankId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SmsModemBlocks", "SimBankId");
        }
    }
}

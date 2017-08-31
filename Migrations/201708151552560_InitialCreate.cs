namespace SmsModemClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsModemBlocks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Operator = c.String(),
                        TelNumber = c.String(),
                        SignalLevel = c.Int(nullable: false),
                        MacAddress = c.String(),
                        ConnectionCheckDelay = c.Int(nullable: false),
                        LogLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsModemBlocks");
        }
    }
}

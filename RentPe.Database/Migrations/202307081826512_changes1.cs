namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomOffers", "RentingPeriod", c => c.Int(nullable: false));
            AlterColumn("dbo.Conversations", "IsOffer", c => c.Boolean(nullable: false));
            DropColumn("dbo.CustomOffers", "RentingPreiod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomOffers", "RentingPreiod", c => c.Int(nullable: false));
            AlterColumn("dbo.Conversations", "IsOffer", c => c.String());
            DropColumn("dbo.CustomOffers", "RentingPeriod");
        }
    }
}

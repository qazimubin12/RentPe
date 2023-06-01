namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "AvailableFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ads", "AvailableTo", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ads", "Authenticity", c => c.String());
            AddColumn("dbo.Ads", "AdStatus", c => c.String());
            AddColumn("dbo.Ads", "RentingPeriod", c => c.Int(nullable: false));
            AddColumn("dbo.Ads", "Featured", c => c.String());
            AddColumn("dbo.RentItems", "Authenticity", c => c.String());
            AddColumn("dbo.RentItems", "Featured", c => c.String());
            AddColumn("dbo.UserInfoes", "UserID", c => c.String());
            AddColumn("dbo.UserInfoes", "Rating", c => c.String());
            AlterColumn("dbo.RentItems", "Negotiable", c => c.String());
            DropColumn("dbo.RentItems", "Favourites");
            DropColumn("dbo.RentItems", "ItemSize");
            DropColumn("dbo.RentItems", "ItemColor");
            DropColumn("dbo.RentItems", "Brand");
            DropColumn("dbo.RentItems", "Note");
            DropColumn("dbo.RentItems", "Label");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentItems", "Label", c => c.String());
            AddColumn("dbo.RentItems", "Note", c => c.String());
            AddColumn("dbo.RentItems", "Brand", c => c.String());
            AddColumn("dbo.RentItems", "ItemColor", c => c.String());
            AddColumn("dbo.RentItems", "ItemSize", c => c.String());
            AddColumn("dbo.RentItems", "Favourites", c => c.Boolean(nullable: false));
            AlterColumn("dbo.RentItems", "Negotiable", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserInfoes", "Rating");
            DropColumn("dbo.UserInfoes", "UserID");
            DropColumn("dbo.RentItems", "Featured");
            DropColumn("dbo.RentItems", "Authenticity");
            DropColumn("dbo.Ads", "Featured");
            DropColumn("dbo.Ads", "RentingPeriod");
            DropColumn("dbo.Ads", "AdStatus");
            DropColumn("dbo.Ads", "Authenticity");
            DropColumn("dbo.Ads", "AvailableTo");
            DropColumn("dbo.Ads", "AvailableFrom");
        }
    }
}

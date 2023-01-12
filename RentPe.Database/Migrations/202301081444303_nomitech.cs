namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomitech : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomOffers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Owner = c.Int(nullable: false),
                        OfferDate = c.DateTime(nullable: false),
                        Rentee = c.Int(nullable: false),
                        Item = c.Int(nullable: false),
                        OfferedPrice = c.Single(nullable: false),
                        Status = c.String(),
                        RentingDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        RentingPreiod = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ItemImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RentItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        UserID = c.String(),
                        Favourites = c.Boolean(nullable: false),
                        ItemDescription = c.String(),
                        ItemCategory = c.String(),
                        RentingDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        ItemSize = c.String(),
                        ItemColor = c.String(),
                        Negotiable = c.Boolean(nullable: false),
                        AvailableFrom = c.DateTime(nullable: false),
                        AvailableTo = c.DateTime(nullable: false),
                        Brand = c.String(),
                        Condition = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        RentingPeriod = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Photo = c.String(),
                        NIC = c.String(),
                        ContactNo = c.String(),
                        MemberSince = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
            DropTable("dbo.RentItems");
            DropTable("dbo.ItemImages");
            DropTable("dbo.ItemCategories");
            DropTable("dbo.CustomOffers");
        }
    }
}

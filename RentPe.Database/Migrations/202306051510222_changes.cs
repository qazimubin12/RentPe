namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdID = c.Int(nullable: false),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.ItemImages");
            DropTable("dbo.RentItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RentItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        UserID = c.String(),
                        ItemDescription = c.String(),
                        ItemCategory = c.String(),
                        Negotiable = c.String(),
                        AvailableFrom = c.DateTime(nullable: false),
                        AvailableTo = c.DateTime(nullable: false),
                        Authenticity = c.String(),
                        Condition = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        RentingPeriod = c.Int(nullable: false),
                        Featured = c.String(),
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
            
            DropTable("dbo.AdImages");
        }
    }
}

namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ads : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Contact = c.String(),
                        Privacy = c.Boolean(nullable: false),
                        ItemName = c.String(),
                        UserID = c.String(),
                        ItemDescription = c.String(),
                        ItemCategory = c.String(),
                        Type = c.String(),
                        Negotiable = c.Boolean(nullable: false),
                        Condition = c.String(),
                        EntryDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        Price = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.RentItems", "RentingDate");
            DropColumn("dbo.RentItems", "ReturnDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RentItems", "ReturnDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RentItems", "RentingDate", c => c.DateTime(nullable: false));
            DropTable("dbo.Ads");
        }
    }
}

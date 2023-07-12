namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alldetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Proof = c.String(),
                        Remarks = c.String(),
                        Name = c.String(),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "VideoOfUnboxing", c => c.String());
            AddColumn("dbo.Orders", "VideoOfPacking", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "VideoOfPacking");
            DropColumn("dbo.Orders", "VideoOfUnboxing");
            DropTable("dbo.Payments");
        }
    }
}

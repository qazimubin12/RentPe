namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        Owner = c.String(),
                        AmountPaid = c.Single(nullable: false),
                        AmountRemain = c.Single(nullable: false),
                        TotalAmount = c.Single(nullable: false),
                        Renter = c.String(),
                        Item = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}

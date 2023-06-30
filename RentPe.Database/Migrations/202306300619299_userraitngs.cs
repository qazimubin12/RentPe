namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userraitngs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Stars = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserRatings");
        }
    }
}

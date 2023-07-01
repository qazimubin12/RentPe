namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesmoreindashboard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        SentBy = c.String(),
                        RecievedBy = c.String(),
                        Attachments = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.CustomOffers", "Owner", c => c.String());
            AlterColumn("dbo.CustomOffers", "Rentee", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomOffers", "Rentee", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomOffers", "Owner", c => c.Int(nullable: false));
            DropTable("dbo.Conversations");
        }
    }
}

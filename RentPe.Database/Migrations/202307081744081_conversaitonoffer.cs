namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conversaitonoffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "IsOffer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "IsOffer");
        }
    }
}

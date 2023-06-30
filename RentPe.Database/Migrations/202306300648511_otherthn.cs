namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otherthn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRatings", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRatings", "Message");
        }
    }
}

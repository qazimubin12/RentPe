namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class includesmessagesdatauserinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserInfoes", "City", c => c.String());
            DropColumn("dbo.UserInfoes", "ContactNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "ContactNo", c => c.String());
            DropColumn("dbo.UserInfoes", "City");
            DropColumn("dbo.Conversations", "Date");
        }
    }
}

namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tempconnectionID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "TempConnectionID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "TempConnectionID");
        }
    }
}

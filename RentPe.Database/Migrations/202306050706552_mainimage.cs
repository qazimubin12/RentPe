namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mainimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "MainImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "MainImage");
        }
    }
}

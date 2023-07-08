namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sentbyname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "SentByName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "SentByName");
        }
    }
}

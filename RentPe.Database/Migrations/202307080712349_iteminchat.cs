namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iteminchat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "Item", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Conversations", "Item");
        }
    }
}

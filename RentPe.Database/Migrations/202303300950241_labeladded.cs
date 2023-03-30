namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class labeladded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentItems", "Label", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentItems", "Label");
        }
    }
}

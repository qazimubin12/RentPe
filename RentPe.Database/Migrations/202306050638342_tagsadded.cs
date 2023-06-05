﻿namespace RentPe.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tagsadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Tag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Tag");
        }
    }
}

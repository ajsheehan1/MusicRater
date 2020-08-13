namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedStoreRating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Store", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Store", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

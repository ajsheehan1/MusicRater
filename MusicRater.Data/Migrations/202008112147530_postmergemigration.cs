namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postmergemigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoreRating",
                c => new
                    {
                        StoreRatingId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        StoreIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StoreRatingId)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            AddColumn("dbo.Store", "StoreRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Store", "CulumativeRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Store", "NumberOfRatings", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreRating", "StoreId", "dbo.Store");
            DropIndex("dbo.StoreRating", new[] { "StoreId" });
            DropColumn("dbo.Store", "NumberOfRatings");
            DropColumn("dbo.Store", "CulumativeRating");
            DropColumn("dbo.Store", "StoreRating");
            DropTable("dbo.StoreRating");
        }
    }
}

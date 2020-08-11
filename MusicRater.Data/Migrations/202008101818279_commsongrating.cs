namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commsongrating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumRating",
                c => new
                    {
                        AlbumRatingId = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        AlbumIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumRatingId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            AddColumn("dbo.Album", "CulumativeRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Album", "NumberOfRatings", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumRating", "AlbumId", "dbo.Album");
            DropIndex("dbo.AlbumRating", new[] { "AlbumId" });
            DropColumn("dbo.Album", "NumberOfRatings");
            DropColumn("dbo.Album", "CulumativeRating");
            DropTable("dbo.AlbumRating");
        }
    }
}

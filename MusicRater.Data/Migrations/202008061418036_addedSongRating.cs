namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSongRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SongRating",
                c => new
                    {
                        SongRatingId = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        SongIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SongRatingId)
                .ForeignKey("dbo.Song", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId);
            
            AddColumn("dbo.Song", "CulumativeRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Song", "NumberOfRatings", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongRating", "SongId", "dbo.Song");
            DropIndex("dbo.SongRating", new[] { "SongId" });
            DropColumn("dbo.Song", "NumberOfRatings");
            DropColumn("dbo.Song", "CulumativeRating");
            DropTable("dbo.SongRating");
        }
    }
}

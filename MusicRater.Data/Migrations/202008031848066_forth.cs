namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Album", "AlbumName", c => c.String(nullable: false));
            AddColumn("dbo.Album", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Album", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Artist", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Artist", "ArtistName", c => c.String(nullable: false));
            AddColumn("dbo.Artist", "ArtistRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Song", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Song", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Song", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Song", "OwnerId");
            DropColumn("dbo.Song", "Rating");
            DropColumn("dbo.Song", "Title");
            DropColumn("dbo.Artist", "ArtistRating");
            DropColumn("dbo.Artist", "ArtistName");
            DropColumn("dbo.Artist", "OwnerId");
            DropColumn("dbo.Album", "CreatedUtc");
            DropColumn("dbo.Album", "Rating");
            DropColumn("dbo.Album", "AlbumName");
            DropColumn("dbo.Album", "OwnerId");
        }
    }
}

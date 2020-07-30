namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "AlbumName", c => c.String(nullable: false));
            AddColumn("dbo.Album", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Album", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Album", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Artist", "OwnerId", c => c.Guid(nullable: false));
            AddColumn("dbo.Artist", "ArtistName", c => c.String(nullable: false));
            AddColumn("dbo.Artist", "ArtistRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Store", "StoreName", c => c.String(nullable: false));
            AddColumn("dbo.Store", "Address", c => c.String());
            AddColumn("dbo.Store", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Store", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Store", "OwnerId");
            DropColumn("dbo.Store", "Rating");
            DropColumn("dbo.Store", "Address");
            DropColumn("dbo.Store", "StoreName");
            DropColumn("dbo.Artist", "ArtistRating");
            DropColumn("dbo.Artist", "ArtistName");
            DropColumn("dbo.Artist", "OwnerId");
            DropColumn("dbo.Album", "OwnerId");
            DropColumn("dbo.Album", "CreatedUtc");
            DropColumn("dbo.Album", "Rating");
            DropColumn("dbo.Album", "AlbumName");
        }
    }
}

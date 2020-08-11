namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingAlbumArtisttoSong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Song", "ArtistName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Song", "ArtistName");
        }
    }
}

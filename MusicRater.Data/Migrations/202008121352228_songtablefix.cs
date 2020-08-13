namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class songtablefix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Song", "AlbumName");
            DropColumn("dbo.Song", "ArtistName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Song", "ArtistName", c => c.String());
            AddColumn("dbo.Song", "AlbumName", c => c.String());
        }
    }
}

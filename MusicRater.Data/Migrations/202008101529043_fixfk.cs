namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixfk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Album", "ArtistId");
            AddForeignKey("dbo.Album", "ArtistId", "dbo.Artist", "ArtistId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropColumn("dbo.Album", "ArtistId");
        }
    }
}

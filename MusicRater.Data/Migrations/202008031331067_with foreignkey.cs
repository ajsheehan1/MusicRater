namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class withforeignkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Song", "AlbumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Song", "AlbumId");
            AddForeignKey("dbo.Song", "AlbumId", "dbo.Album", "AlbumId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Song", "AlbumId", "dbo.Album");
            DropIndex("dbo.Song", new[] { "AlbumId" });
            DropColumn("dbo.Song", "AlbumId");
        }
    }
}

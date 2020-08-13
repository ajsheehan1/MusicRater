namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostMerge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumInStore",
                c => new
                    {
                        AlbumRefId = c.Int(nullable: false),
                        StoreRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlbumRefId, t.StoreRefId })
                .ForeignKey("dbo.Album", t => t.AlbumRefId, cascadeDelete: true)
                .ForeignKey("dbo.Store", t => t.StoreRefId, cascadeDelete: true)
                .Index(t => t.AlbumRefId)
                .Index(t => t.StoreRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumInStore", "StoreRefId", "dbo.Store");
            DropForeignKey("dbo.AlbumInStore", "AlbumRefId", "dbo.Album");
            DropIndex("dbo.AlbumInStore", new[] { "StoreRefId" });
            DropIndex("dbo.AlbumInStore", new[] { "AlbumRefId" });
            DropTable("dbo.AlbumInStore");
        }
    }
}

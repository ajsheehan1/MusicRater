namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0805 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Song", "AlbumName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Song", "AlbumName");
        }
    }
}

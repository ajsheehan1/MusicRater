namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmigr : DbMigration
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

namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newestmigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Song", "TestColumn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Song", "TestColumn", c => c.Int(nullable: false));
        }
    }
}

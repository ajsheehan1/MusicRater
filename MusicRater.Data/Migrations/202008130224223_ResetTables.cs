namespace MusicRater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResetTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumRating",
                c => new
                    {
                        AlbumRatingId = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        AlbumIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumRatingId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedUtc = c.DateTimeOffset(nullable: false, precision: 7),
                        CulumativeRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfRatings = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        ArtistName = c.String(nullable: false),
                        ArtistRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CulumativeRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfRatings = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(nullable: false),
                        Address = c.String(),
                        OwnerId = c.Guid(nullable: false),
                        StoreRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CulumativeRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfRatings = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreId);
            
            CreateTable(
                "dbo.ArtistRating",
                c => new
                    {
                        ArtistRatingId = c.Int(nullable: false, identity: true),
                        ArtistId = c.Int(nullable: false),
                        ArtistIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistRatingId)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.SongRating",
                c => new
                    {
                        SongRatingId = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        SongIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SongRatingId)
                .ForeignKey("dbo.Song", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.Song",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        SongName = c.String(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CulumativeRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfRatings = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Album", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.StoreRating",
                c => new
                    {
                        StoreRatingId = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        StoreIndividualRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StoreRatingId)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
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
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.StoreRating", "StoreId", "dbo.Store");
            DropForeignKey("dbo.SongRating", "SongId", "dbo.Song");
            DropForeignKey("dbo.Song", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.ArtistRating", "ArtistId", "dbo.Artist");
            DropForeignKey("dbo.AlbumRating", "AlbumId", "dbo.Album");
            DropForeignKey("dbo.AlbumInStore", "StoreRefId", "dbo.Store");
            DropForeignKey("dbo.AlbumInStore", "AlbumRefId", "dbo.Album");
            DropForeignKey("dbo.Album", "ArtistId", "dbo.Artist");
            DropIndex("dbo.AlbumInStore", new[] { "StoreRefId" });
            DropIndex("dbo.AlbumInStore", new[] { "AlbumRefId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.StoreRating", new[] { "StoreId" });
            DropIndex("dbo.Song", new[] { "AlbumId" });
            DropIndex("dbo.SongRating", new[] { "SongId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.ArtistRating", new[] { "ArtistId" });
            DropIndex("dbo.Album", new[] { "ArtistId" });
            DropIndex("dbo.AlbumRating", new[] { "AlbumId" });
            DropTable("dbo.AlbumInStore");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.StoreRating");
            DropTable("dbo.Song");
            DropTable("dbo.SongRating");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.ArtistRating");
            DropTable("dbo.Store");
            DropTable("dbo.Artist");
            DropTable("dbo.Album");
            DropTable("dbo.AlbumRating");
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace MusicRater.Data
{//-----------------
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //########### ONLY USING ApplicationDbContext; NOT AlbumDbContext
    //public class AlbumDbContext : IdentityDbContext
    //{
    //    public AlbumDbContext() : base("AlbumDbDefault")
    //    {
    //    }

    //    public DbSet<Album> Albums { get; set; }
    //    public DbSet<Store> Stores { get; set; }

    //    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    //{
    //    //    base.OnModelCreating(modelBuilder);
    //    //}

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {

    //        modelBuilder.Entity<Album>()
    //                    .HasMany<Store>(s => s.Stores)
    //                    .WithMany(c => c.Albums)
    //                    .Map(cs =>
    //                    {
    //                        cs.MapLeftKey("AlbumRefId");
    //                        cs.MapRightKey("StoreRefId");
    //                        cs.ToTable("AlbumInStore");
    //                    });
    //        modelBuilder
    //            .Conventions
    //            .Remove<PluralizingTableNameConvention>();

    //        modelBuilder
    //            .Configurations
    //            .Add(new IdentityUserLoginConfiguration())
    //            .Add(new IdentityUserRoleConfiguration());

    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        // This is where we will add our DBsets 
        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistRating> ArtistRatings { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumRating> AlbumRatings { get; set; }
        public DbSet<Song> Songs { get; set; }

        public DbSet<SongRating> SongRatings { get; set; }

        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreRating> StoreRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Added the following modelBuilder for many2many with Store and Album
            modelBuilder.Entity<Album>()
            .HasMany<Store>(s => s.Stores)
            .WithMany(c => c.Albums)
            .Map(cs =>
            {
                cs.MapLeftKey("AlbumRefId");
                cs.MapRightKey("StoreRefId");
                cs.ToTable("AlbumInStore");
            });

            modelBuilder
            .Conventions
            .Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Configurations
                .Add(new IdentityUserLoginConfiguration())
                .Add(new IdentityUserRoleConfiguration());
        }
    }

    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }
    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }

    }
}
    using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicRater.Services
{
    public class ArtistService
    {
        private readonly Guid _userId;

        public ArtistService(Guid userId)
        {
            _userId = userId;
        }

        public ArtistService()
        {
        }

        public bool CreateArtist(ArtistCreate model)
        {
            var entity =
                new Artist()
                {
                    OwnerId = _userId,
                    ArtistName = model.ArtistName,
                    ArtistRating = model.ArtistRating
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Artists.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        } // CreateArtist

        public IEnumerable<ArtistListItem> GetArtists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Artists
                        //.Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ArtistListItem
                                {
                                    ArtistId = e.ArtistId,
                                    ArtistName = e.ArtistName,
                                    ArtistRating = e.ArtistRating,
                                    OwnerId = e.OwnerId
                                }
                        );

                return query.ToArray();
            }
        } // GetArtists

        public ArtistDetail GetArtistById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Artists
                        .Single(e => e.ArtistId == id);
                        //.Single(e => e.ArtistId == id && e.OwnerId == _userId);
                return
                    new ArtistDetail
                    {
                        ArtistId = entity.ArtistId,
                        ArtistName = entity.ArtistName,
                        ArtistRating = entity.ArtistRating,
                        CulumativeRating = entity.CulumativeRating,
                        NumberOfRatings = entity.NumberOfRatings
                    };
            }

        } // GetArtistById

        public bool UpdateArtist(ArtistEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Artists
                        .Single(e => e.ArtistId == model.ArtistId);
                        //.Single(e => e.ArtistId == model.ArtistId && e.OwnerId == _userId);

                entity.ArtistName = model.ArtistName;
                entity.ArtistRating = model.ArtistRating;
                entity.CulumativeRating = model.CulumativeRating;
                entity.NumberOfRatings = model.NumberOfRatings;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateArtist

        public bool DeleteArtist(int artistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Artists
                        .Single(e => e.ArtistId == artistId && e.OwnerId == _userId);

                ctx.Artists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        } // DeleteArtist

    } // class ArtistService
} // namespace

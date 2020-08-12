using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class ArtistRatingService
    {
        private readonly Guid _userId;

        public ArtistRatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateArtistRating(ArtistRatingCreate model)
        {
            // format the new ArtistRating record
            var entity =
                new ArtistRating()
                {
                    OwnerId = _userId,
                    ArtistId = model.ArtistId,
                    ArtistIndividualRating = model.ArtistIndividualRating,
                    Artist = model.Artist
                };
            // Add the new ArtistRating to the table
            using (var ctx = new ApplicationDbContext())
            {
                ctx.ArtistRatings.Add(entity);
                bool addedArtistRating = ctx.SaveChanges() == 1;
                if (!addedArtistRating)
                {
                    return false;
                }

            }

            // Update the Artist record 
            using (var ctx = new ApplicationDbContext())
            {
                // retrieve the ArtistRating record we just posted so we can follow the foreign key to the Artist record
                var newArtistRating =
                    ctx
                        .ArtistRatings
                        .Single(e => e.ArtistRatingId == entity.ArtistRatingId && e.OwnerId == _userId);

                // Update the fields in the Artist record at the other end of the foreign key
                newArtistRating.Artist.CulumativeRating += model.ArtistIndividualRating;
                newArtistRating.Artist.NumberOfRatings += 1;

                newArtistRating.Artist.ArtistRating = newArtistRating.Artist.CulumativeRating / newArtistRating.Artist.NumberOfRatings;

                return ctx.SaveChanges() == 1;
            }

        } // CreateArtistRating


        public IEnumerable<ArtistRatingListItem> GetArtistRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ArtistRatings
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ArtistRatingListItem
                                {
                                    ArtistRatingId = e.ArtistRatingId,
                                    ArtistId = e.ArtistId,
                                    ArtistIndividualRating = e.ArtistIndividualRating,
                                    OwnerId = e.OwnerId
                                }
                        );

                return query.ToArray();
            }
        } // GetArtistRatings

        public IEnumerable<ArtistRatingByArtist> GetRatingsByArtist(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .ArtistRatings
                        .Where(e => e.ArtistId == id)
                        .Select(
                            e =>
                                new ArtistRatingByArtist
                                {
                                    ArtistRatingId = e.ArtistRatingId,
                                    ArtistId = e.ArtistId,
                                    ArtistIndividualRating = e.ArtistIndividualRating,
                                    OwnerId = e.OwnerId

                                }
                        );

                return query.ToArray();
            }
        } // GetRatingsByArtist

        public ArtistRatingEdit GetArtistRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ArtistRatings
                        .Single(e => e.ArtistRatingId == id);
                //.Single(e => e.ArtistRatingId == id && e.OwnerId == _userId);
                return
                    new ArtistRatingEdit
                    {
                        ArtistRatingId = entity.ArtistRatingId,
                        ArtistId = entity.ArtistId,
                        ArtistIndividualRating = entity.ArtistIndividualRating
                    };
            }

        } // GetArtistRatingById



        public bool UpdateArtistRating(ArtistRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ArtistRatings
                        .Single(e => e.ArtistRatingId == model.ArtistRatingId && e.OwnerId == _userId);


                entity.Artist.CulumativeRating = entity.Artist.CulumativeRating - entity.ArtistIndividualRating;
                entity.Artist.CulumativeRating = model.ArtistIndividualRating + entity.Artist.CulumativeRating;
                entity.Artist.ArtistRating = entity.Artist.CulumativeRating / entity.Artist.NumberOfRatings;

                entity.ArtistId = model.ArtistId;
                entity.ArtistIndividualRating = model.ArtistIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateArtistRating


        public bool DeleteArtistRating(int artistRatingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ArtistRatings
                        .Single(e => e.ArtistRatingId == artistRatingId && e.OwnerId == _userId);

                entity.Artist.CulumativeRating -= entity.ArtistIndividualRating;
                entity.Artist.NumberOfRatings -= 1;

                if (entity.Artist.NumberOfRatings == 0)
                {
                    entity.Artist.ArtistRating = 0;
                }
                else
                {
                    entity.Artist.ArtistRating = entity.Artist.CulumativeRating / entity.Artist.NumberOfRatings;
                }

                ctx.ArtistRatings.Remove(entity);
                return ctx.SaveChanges() == 2;
            }

        } // DeleteArtistRating

    }  // public class ArtistRatingService
} // namespace MusicRater.Services

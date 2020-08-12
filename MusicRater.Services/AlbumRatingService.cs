using System;
using MusicRater.Data;
using MusicRater.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class AlbumRatingService
    {
        private readonly Guid _userId;
        public AlbumRatingService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateAlbumRating(AlbumRatingCreate model)
        {
            // format the new AlbumRating record
            var entity =
                new AlbumRating()
                {
                    OwnerId = _userId,
                    AlbumId = model.AlbumId,
                    AlbumIndividualRating = model.AlbumIndividualRating,
                    Album = model.Album
                };

            // Add the new AlbumRating to the table
            using (var ctx = new ApplicationDbContext())
            {
                ctx.AlbumRatings.Add(entity);
                bool addedAlbumRating = ctx.SaveChanges() == 1;
                if (!addedAlbumRating)
                {
                    return false;
                }

            }

            // Update the Album record 
            using (var ctx = new ApplicationDbContext())
            {
                // retrieve the AlbumRating record we just posted so we can follow the foreign key to the Album record
                var newAlbumRating =
                    ctx
                        .AlbumRatings
                        .Single(e => e.AlbumRatingId == entity.AlbumRatingId && e.OwnerId == _userId);

                // Update the fields in the Album record at the other end of the foreign key
                newAlbumRating.Album.CulumativeRating += model.AlbumIndividualRating;
                newAlbumRating.Album.NumberOfRatings += 1;

                newAlbumRating.Album.Rating = newAlbumRating.Album.CulumativeRating / newAlbumRating.Album.NumberOfRatings;

                return ctx.SaveChanges() == 1;
            }

        } // CreateAlbumRating


        public IEnumerable<AlbumRatingListItem> GetAlbumRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .AlbumRatings
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new AlbumRatingListItem
                                {
                                    AlbumRatingId = e.AlbumRatingId,
                                    AlbumId = e.AlbumId,
                                    AlbumIndividualRating = e.AlbumIndividualRating,
                                    OwnerId = e.OwnerId
                                }
                        );

                return query.ToArray();
            }
        } // GetAlbumRatings
        public IEnumerable<AlbumRatingByAlbum> GetRatingsByAlbum(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .AlbumRatings
                        .Where(e => e.AlbumId == id)
                        .Select(
                            e =>
                                new AlbumRatingByAlbum
                                {
                                    AlbumRatingId = e.AlbumRatingId,
                                    AlbumIndividualRating = e.AlbumIndividualRating,
                                    OwnerId = e.OwnerId,
                                    AlbumId = e.AlbumId,
                                }
                        );

                return query.ToArray();
            }
        } // GetRatingsByAlbum

        public AlbumRatingEdit GetAlbumRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AlbumRatings
                        .Single(e => e.AlbumRatingId == id);
                //.Single(e => e.AlbumRatingId == id && e.OwnerId == _userId);
                return
                    new AlbumRatingEdit
                    {
                        AlbumRatingId = entity.AlbumRatingId,
                        AlbumId = entity.AlbumId,
                        AlbumIndividualRating = entity.AlbumIndividualRating
                    };
            }

        } // GetAlbumRatingById



        public bool UpdateAlbumRating(AlbumRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AlbumRatings
                        .Single(e => e.AlbumRatingId == model.AlbumRatingId && e.OwnerId == _userId);


                entity.Album.CulumativeRating = entity.Album.CulumativeRating - entity.AlbumIndividualRating;
                entity.Album.CulumativeRating = model.AlbumIndividualRating + entity.Album.CulumativeRating;
                entity.Album.Rating = entity.Album.CulumativeRating / entity.Album.NumberOfRatings;

                entity.AlbumId = model.AlbumId;
                entity.AlbumIndividualRating = model.AlbumIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateAlbumRating

        public bool DeleteAlbumRating(int albumRatingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AlbumRatings
                        .Single(e => e.AlbumRatingId == albumRatingId && e.OwnerId == _userId);

                entity.Album.CulumativeRating -= entity.AlbumIndividualRating;
                entity.Album.NumberOfRatings -= 1;

                if (entity.Album.NumberOfRatings == 0)
                {
                    entity.Album.Rating = 0;
                }
                else
                {
                    entity.Album.Rating = entity.Album.CulumativeRating / entity.Album.NumberOfRatings;
                }
                ctx.AlbumRatings.Remove(entity);

                return ctx.SaveChanges() == 2;
            }

        } // DeleteAlbumRating
    }
}



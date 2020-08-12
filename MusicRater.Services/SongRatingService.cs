using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class SongRatingService
    {
        private readonly Guid _userId;

        public SongRatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSongRating(SongRatingCreate model)
        {
            // format the new SongRating record
            var entity =
                new SongRating()
                {
                    OwnerId = _userId,
                    SongId = model.SongId,
                    SongIndividualRating = model.SongIndividualRating,
                    Song = model.Song
                };
            // Add the new SongRating to the table
            using (var ctx = new ApplicationDbContext())
            {
                ctx.SongRatings.Add(entity);
                bool addedSongRating = ctx.SaveChanges() == 1;
                if (!addedSongRating)
                {
                    return false;
                }

            }

            // Update the Song record 
            using (var ctx = new ApplicationDbContext())
            {
                // retrieve the SongRating record we just posted so we can follow the foreign key to the Song record
                var newSongRating =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == entity.SongRatingId && e.OwnerId == _userId);

                // Update the fields in the Song record at the other end of the foreign key
                newSongRating.Song.CulumativeRating += model.SongIndividualRating;
                newSongRating.Song.NumberOfRatings += 1;

                newSongRating.Song.Rating = newSongRating.Song.CulumativeRating / newSongRating.Song.NumberOfRatings;

                return ctx.SaveChanges() == 1;
            }
        } // CreateSongRating
        public IEnumerable<SongRatingListItem> GetSongRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SongRatings
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SongRatingListItem
                                {
                                    SongRatingId = e.SongRatingId,
                                    SongId = e.SongId,
                                    SongIndividualRating = e.SongIndividualRating,
                                    OwnerId = e.OwnerId
                                }
                        );

                return query.ToArray();
            }
        } // GetArtistRatings

        public IEnumerable<SongRatingBySong> GetRatingsBySong(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .SongRatings
                        .Where(e => e.SongId == id)
                        .Select(
                            e =>
                                new SongRatingBySong
                                {
                                    SongRatingId = e.SongRatingId,
                                    SongId = e.SongId,
                                    SongIndividualRating = e.SongIndividualRating,
                                    OwnerId = e.OwnerId

                                }
                        );

                return query.ToArray();
            }
        } // GetRatingsBySong

        public SongRatingEdit GetSongRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == id);
                //.Single(e => e.ArtistRatingId == id && e.OwnerId == _userId);
                return
                    new SongRatingEdit
                    {
                        SongRatingId = entity.SongRatingId,
                        SongId = entity.SongId,
                        SongIndividualRating = entity.SongIndividualRating
                    };
            }

        } // GetsongRatingById

        public bool UpdateSongRating(SongRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == model.SongRatingId && e.OwnerId == _userId);

                
                entity.Song.CulumativeRating = entity.Song.CulumativeRating - entity.SongIndividualRating;
                entity.Song.CulumativeRating = model.SongIndividualRating + entity.Song.CulumativeRating;
                entity.Song.Rating = entity.Song.CulumativeRating / entity.Song.NumberOfRatings;

                entity.SongId = model.SongId;
                entity.SongIndividualRating = model.SongIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateArtistRating

        public bool DeleteSongRating(int songRatingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == songRatingId && e.OwnerId == _userId);
                
                entity.Song.CulumativeRating -= entity.SongIndividualRating;
                entity.Song.NumberOfRatings -= 1;

                if (entity.Song.NumberOfRatings == 0)
                {
                    entity.Song.Rating = 0;
                }
                else
                {
                    entity.Song.Rating = entity.Song.CulumativeRating / entity.Song.NumberOfRatings;
                }

                ctx.SongRatings.Remove(entity);
                return ctx.SaveChanges() == 2;
            }

        } // DeleteArtistRating
    }
}
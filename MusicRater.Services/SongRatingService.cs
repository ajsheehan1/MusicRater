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
                    SongIndividualRating = model.SongIndividualRating
                };

            // retrieve the Song record to which the Rating refers
            var updateSongRating = new SongService();
            SongDetail songDetail = updateSongRating.GetSongById(model.SongId);

            // Format an object of type SongEdit 
            // > increase the Cumulative Rating by this SongIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the SongRating field
            SongEdit songUpdate = new SongEdit();

            songUpdate.SongId = songDetail.SongId;
            songUpdate.SongName = songDetail.SongName;

            songUpdate.CulumativeRating = songDetail.CulumativeRating + model.SongIndividualRating;
            songUpdate.NumberOfRatings = songDetail.NumberOfRatings + 1;
            decimal newCumulativeRating = songUpdate.CulumativeRating;
            int newNumberOfRatings = songUpdate.NumberOfRatings;
            songUpdate.Rating = newCumulativeRating / newNumberOfRatings;

            // update the Song which is represented in this new SongRating
            bool updated = updateSongRating.UpdateRatingAverage(songUpdate);


        //songDetail.CulumativeRating += model.SongIndividualRating;
        //songDetail.NumberRatings += 1;

            // Call CalculateSongAvgRating with the current model
            // inside the method, calcuate the average of all Ratings for the Song in the model
            // return an object of type SongEdit - to retain the song name and ID, and it will have the average Rating in the Rating field
            //SongEdit songWithAvgRating = CalculateSongAvgRating(model);

            //bool SongRatingUpdated = UpdateRatingInSong(songWithAvgRating);


            using (var ctx = new ApplicationDbContext())
            {
                ctx.SongRatings.Add(entity);
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
        } // GetSongRatings

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
                //.Single(e => e.SongRatingId == id && e.OwnerId == _userId);
                return
                    new SongRatingEdit
                    {
                        SongRatingId = entity.SongRatingId,
                        SongId = entity.SongId,
                        SongIndividualRating = entity.SongIndividualRating
                    };
            }

        } // GetSongRatingById



        public bool UpdateSongRating(SongRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == model.SongRatingId && e.OwnerId == _userId);
               

                entity.SongId = model.SongId;
                entity.SongIndividualRating = model.SongIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateSongRating

        public bool DeleteSongRating(int songRatingId)
        {
            // since we're just passing in the id of the SongRating to be deleted,
            // we need to get the SongRating record so we know what amount to deduct from the 
            // CumulativeRating in the Song record
            // retrieve the Song record to which the Rating refers
            // Get SongRatingId, SongId, and SongIndividualRating
            SongRatingEdit songRatingToDeduct = GetSongRatingById(songRatingId);

            // retrieve the Song record to which the Rating refers
            var updateSongRating = new SongService();
            SongDetail songDetail = updateSongRating.GetSongById(songRatingToDeduct.SongId);

            // Format an object of type SongEdit 
            // > increase the Cumulative Rating by this SongIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the SongRating field
            SongEdit songUpdate = new SongEdit();

            songUpdate.SongId = songDetail.SongId;
            songUpdate.SongName = songDetail.SongName;

            songUpdate.CulumativeRating = songDetail.CulumativeRating - songRatingToDeduct.SongIndividualRating;
            songUpdate.NumberOfRatings = songDetail.NumberOfRatings - 1;
            decimal newCumulativeRating = songUpdate.CulumativeRating;
            int newNumberOfRatings = songUpdate.NumberOfRatings;
            songUpdate.Rating = newCumulativeRating / newNumberOfRatings;

            // update the Song which is represented in this new SongRating
            bool updated = updateSongRating.UpdateRatingAverage(songUpdate);




            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .SongRatings
                        .Single(e => e.SongRatingId == songRatingId && e.OwnerId == _userId);

                ctx.SongRatings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }

        } // DeleteSongRating

    }
}

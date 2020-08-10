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
                    AlbumIndividualRating = model.AlbumIndividualRating
                };

            // retrieve the Album record to which the Rating refers
            var updateAlbumRating = new AlbumService();
            AlbumDetails albumDetail = updateAlbumRating.GetAlbumById(model.AlbumId);

            // Format an object of type AlbumEdit 
            // > increase the Cumulative Rating by this AlbumIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the AlbumRating field
            AlbumEdit albumUpdate = new AlbumEdit();

            albumUpdate.AlbumId = albumDetail.AlbumId;
            albumUpdate.AlbumName = albumDetail.AlbumName;

            albumUpdate.CulumativeRating = albumDetail.CulumativeRating + model.AlbumIndividualRating;
            albumUpdate.NumberOfRatings = albumDetail.NumberOfRatings + 1;
            decimal newCumulativeRating = albumUpdate.CulumativeRating;
            int newNumberOfRatings = albumUpdate.NumberOfRatings;
            albumUpdate.Rating = newCumulativeRating / newNumberOfRatings;

            // update the Album which is represented in this new AlbumRating
            bool updated = updateAlbumRating.UpdateRatingAverage(albumUpdate);


            //albumDetail.CulumativeRating += model.AlbumIndividualRating;
            //albumDetail.NumberRatings += 1;

            // Call CalculateAlbumAvgRating with the current model
            // inside the method, calcuate the average of all Ratings for the Album in the model
            // return an object of type AlbumEdit - to retain the album name and ID, and it will have the average Rating in the Rating field
            //AlbumEdit albumWithAvgRating = CalculateAlbumAvgRating(model);

            //bool AlbumRatingUpdated = UpdateRatingInAlbum(albumWithAvgRating);


            using (var ctx = new ApplicationDbContext())
            {
                ctx.AlbumRatings.Add(entity);
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
                                    OwnerId = e.OwnerId

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


                entity.AlbumId = model.AlbumId;
                entity.AlbumIndividualRating = model.AlbumIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateAlbumRating

        public bool DeleteAlbumRating(int albumRatingId)
        {
            // since we're just passing in the id of the AlbumRating to be deleted,
            // we need to get the AlbumRating record so we know what amount to deduct from the 
            // CumulativeRating in the Album record
            // retrieve the Album record to which the Rating refers
            // Get AlbumRatingId, AlbumId, and AlbumIndividualRating
            AlbumRatingEdit albumRatingToDeduct = GetAlbumRatingById(albumRatingId);

            // retrieve the Album record to which the Rating refers
            var updateAlbumRating = new AlbumService();
            AlbumDetails albumDetail = updateAlbumRating.GetAlbumById(albumRatingToDeduct.AlbumId);

            // Format an object of type AlbumEdit 
            // > increase the Cumulative Rating by this AlbumIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the AlbumRating field
            AlbumEdit albumUpdate = new AlbumEdit();

            albumUpdate.AlbumId = albumDetail.AlbumId;
            albumUpdate.AlbumName = albumDetail.AlbumName;

            albumUpdate.CulumativeRating = albumDetail.CulumativeRating - albumRatingToDeduct.AlbumIndividualRating;
            albumUpdate.NumberOfRatings = albumDetail.NumberOfRatings - 1;
            decimal newCumulativeRating = albumUpdate.CulumativeRating;
            int newNumberOfRatings = albumUpdate.NumberOfRatings;
            albumUpdate.Rating = newCumulativeRating / newNumberOfRatings;

            // update the Album which is represented in this new AlbumRating
            bool updated = updateAlbumRating.UpdateRatingAverage(albumUpdate);




            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .AlbumRatings
                        .Single(e => e.AlbumRatingId == albumRatingId && e.OwnerId == _userId);

                ctx.AlbumRatings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }

        } // DeleteAlbumRating

    }
}



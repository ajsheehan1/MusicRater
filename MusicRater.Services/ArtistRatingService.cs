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
                    ArtistIndividualRating = model.ArtistIndividualRating
                };

            // retrieve the Artist record to which the Rating refers
            var updateArtistRating = new ArtistService();
            ArtistDetail artistDetail = updateArtistRating.GetArtistById(model.ArtistId);

            // Format an object of type ArtistEdit 
            // > increase the Cumulative Rating by this ArtistIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the ArtistRating field
            ArtistEdit artistUpdate = new ArtistEdit();

            artistUpdate.ArtistId = artistDetail.ArtistId;
            artistUpdate.ArtistName = artistDetail.ArtistName;

            artistUpdate.CulumativeRating = artistDetail.CulumativeRating + model.ArtistIndividualRating;
            artistUpdate.NumberOfRatings = artistDetail.NumberOfRatings + 1;
            decimal newCumulativeRating = artistUpdate.CulumativeRating;
            int newNumberOfRatings = artistUpdate.NumberOfRatings;
            artistUpdate.ArtistRating = newCumulativeRating / newNumberOfRatings;

            // update the Artist which is represented in this new ArtistRating
            bool updated = updateArtistRating.UpdateArtist(artistUpdate);


        //artistDetail.CulumativeRating += model.ArtistIndividualRating;
        //artistDetail.NumberRatings += 1;

            // Call CalculateArtistAvgRating with the current model
            // inside the method, calcuate the average of all Ratings for the Artist in the model
            // return an object of type ArtistEdit - to retain the artist name and ID, and it will have the average Rating in the Rating field
            //ArtistEdit artistWithAvgRating = CalculateArtistAvgRating(model);

            //bool ArtistRatingUpdated = UpdateRatingInArtist(artistWithAvgRating);


            using (var ctx = new ApplicationDbContext())
            {
                ctx.ArtistRatings.Add(entity);
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

                entity.ArtistId = model.ArtistId;
                entity.ArtistIndividualRating = model.ArtistIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateArtistRating

        public bool DeleteArtistRating(int artistRatingId)
        {
            // since we're just passing in the id of the ArtistRating to be deleted,
            // we need to get the ArtistRating record so we know what amount to deduct from the 
            // CumulativeRating in the Artist record
            // retrieve the Artist record to which the Rating refers
            // Get ArtistRatingId, ArtistId, and ArtistIndividualRating
            ArtistRatingEdit artistRatingToDeduct = GetArtistRatingById(artistRatingId);

            // retrieve the Artist record to which the Rating refers
            var updateArtistRating = new ArtistService();
            ArtistDetail artistDetail = updateArtistRating.GetArtistById(artistRatingToDeduct.ArtistId);

            // Format an object of type ArtistEdit 
            // > increase the Cumulative Rating by this ArtistIndividualRating
            // > increase the Number of Ratings by 1 (for this rating)
            // > assign the new average rating to the ArtistRating field
            ArtistEdit artistUpdate = new ArtistEdit();

            artistUpdate.ArtistId = artistDetail.ArtistId;
            artistUpdate.ArtistName = artistDetail.ArtistName;

            artistUpdate.CulumativeRating = artistDetail.CulumativeRating - artistRatingToDeduct.ArtistIndividualRating;
            artistUpdate.NumberOfRatings = artistDetail.NumberOfRatings - 1;
            decimal newCumulativeRating = artistUpdate.CulumativeRating;
            int newNumberOfRatings = artistUpdate.NumberOfRatings;
            artistUpdate.ArtistRating = newCumulativeRating / newNumberOfRatings;

            // update the Artist which is represented in this new ArtistRating
            bool updated = updateArtistRating.UpdateArtist(artistUpdate);




            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .ArtistRatings
                        .Single(e => e.ArtistRatingId == artistRatingId && e.OwnerId == _userId);

                ctx.ArtistRatings.Remove(entity);

                return ctx.SaveChanges() == 1;
            }

        } // DeleteArtistRating

    }
}

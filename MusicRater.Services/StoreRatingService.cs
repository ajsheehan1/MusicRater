using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class StoreRatingService
    {
        private readonly Guid _userId;

        public StoreRatingService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStoreRating(StoreRatingCreate model)
        {
            // format the new StoreRating record
            var entity =
                new StoreRating()
                {
                    OwnerId = _userId,
                    StoreId = model.StoreId,
                    StoreIndividualRating = model.StoreIndividualRating,
                    Store = model.Store
                };
            // Add the new StoreRating to the table
            using (var ctx = new ApplicationDbContext())
            {
                ctx.StoreRatings.Add(entity);
                bool addedStoreRating = ctx.SaveChanges() == 1;
                if (!addedStoreRating)
                {
                    return false;
                }

            }

            // Update the Store record 
            using (var ctx = new ApplicationDbContext())
            {
                // retrieve the StoreRating record we just posted so we can follow the foreign key to the Store record
                var newStoreRating =
                    ctx
                        .StoreRatings
                        .Single(e => e.StoreRatingId == entity.StoreRatingId && e.OwnerId == _userId);

                // Update the fields in the Store record at the other end of the foreign key
                newStoreRating.Store.CulumativeRating += model.StoreIndividualRating;
                newStoreRating.Store.NumberOfRatings += 1;

                newStoreRating.Store.StoreRating = newStoreRating.Store.CulumativeRating / newStoreRating.Store.NumberOfRatings;

                return ctx.SaveChanges() == 1;
            }

        } // CreateStoreRating


        public IEnumerable<StoreRatingListItem> GetStoreRatings()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .StoreRatings
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new StoreRatingListItem
                                {
                                    StoreRatingId = e.StoreRatingId,
                                    StoreId = e.StoreId,
                                    StoreIndividualRating = e.StoreIndividualRating,
                                    OwnerId = e.OwnerId
                                }
                        );

                return query.ToArray();
            }
        } // GetStoreRatings

        public IEnumerable<StoreRatingByStore> GetRatingsByStore(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .StoreRatings
                        .Where(e => e.StoreId == id)
                        .Select(
                            e =>
                                new StoreRatingByStore
                                {
                                    StoreRatingId = e.StoreRatingId,
                                    StoreIndividualRating = e.StoreIndividualRating,
                                    OwnerId = e.OwnerId,
                                    StoreId = e.StoreId
                                }
                        );

                return query.ToArray();
            }
        } // GetRatingsByStore

        public StoreRatingEdit GetStoreRatingById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .StoreRatings
                        .Single(e => e.StoreRatingId == id);
                //.Single(e => e.StoreRatingId == id && e.OwnerId == _userId);
                return
                    new StoreRatingEdit
                    {
                        StoreRatingId = entity.StoreRatingId,
                        StoreId = entity.StoreId,
                        StoreIndividualRating = entity.StoreIndividualRating
                    };
            }

        } // GetStoreRatingById



        public bool UpdateStoreRating(StoreRatingEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .StoreRatings
                        .Single(e => e.StoreRatingId == model.StoreRatingId && e.OwnerId == _userId);

                entity.Store.CulumativeRating = entity.Store.CulumativeRating - entity.StoreIndividualRating;
                entity.Store.CulumativeRating = model.StoreIndividualRating + entity.Store.CulumativeRating;
                entity.Store.StoreRating = entity.Store.CulumativeRating / entity.Store.NumberOfRatings;

                entity.StoreId = model.StoreId;
                entity.StoreIndividualRating = model.StoreIndividualRating;

                return ctx.SaveChanges() == 1;
            }
        } // UpdateStoreRating


        public bool DeleteStoreRating(int storeRatingId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .StoreRatings
                        .Single(e => e.StoreRatingId == storeRatingId && e.OwnerId == _userId);

                entity.Store.CulumativeRating -= entity.StoreIndividualRating;
                entity.Store.NumberOfRatings -= 1;

                if (entity.Store.NumberOfRatings == 0)
                {
                    entity.Store.StoreRating = 0;
                }
                else
                {
                    entity.Store.StoreRating = entity.Store.CulumativeRating / entity.Store.NumberOfRatings;
                }

                ctx.StoreRatings.Remove(entity);
                return ctx.SaveChanges() == 2;
            }

        } // DeleteStoreRating

    }  // public class StoreRatingService
} // namespace MusicRater.Services

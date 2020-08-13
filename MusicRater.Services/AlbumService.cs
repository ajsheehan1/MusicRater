using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class AlbumService
    {
        private readonly Guid _userId;

        public AlbumService(Guid userId)
        {
            _userId = userId;
        }
        public AlbumService()
        {

        }
        public bool CreateAlbum(AlbumCreate model)
        {
            var entity =
                new Album()
                {
                    OwnerId = _userId,
                    AlbumName = model.AlbumName,
                    Rating = model.Rating,
                    ArtistId = model.ArtistId,
                    //StoreId = model.StoreId
                    //CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Albums.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public List<StoreDetail> GetAllStoresWithAlbum(int albumId, bool getStores)
        {
            if (getStores)
            {
                // You're in the right place
            }
            using (var ctx = new ApplicationDbContext())
            {
                var album =
                    ctx
                        .Albums
                        .Single(e => e.AlbumId == albumId);
                List<StoreDetail> storeList = new List<StoreDetail>();
                var stores = album.Stores.ToArray();
                foreach(var store in stores)
                {
                    StoreDetail listItem = new StoreDetail();
                    listItem.StoreId = store.StoreId;
                    listItem.StoreName = store.StoreName;
                    listItem.Address = store.Address;
                    listItem.StoreRating = store.StoreRating;

                    storeList.Add(listItem);
                }
                return storeList;
            }
        }

        public IEnumerable<ListAlbums> GetAlbums()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Albums
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new ListAlbums
                            {
                                AlbumId = e.AlbumId,
                                AlbumName = e.AlbumName,
                                ArtistName = e.Artist.ArtistName,
                                Rating = e.Rating,
                                ArtistId = e.ArtistId
                                    //CreatedUtc = e.CreatedUtc
                                }
                            );
                return query.ToArray();
            }
        }

        public AlbumDetails GetAlbumById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Albums
                        .Single(e => e.AlbumId == id && e.OwnerId == _userId);
                return
                    new AlbumDetails
                    {
                        AlbumId = entity.AlbumId,
                        AlbumName = entity.AlbumName,
                        ArtistName = entity.Artist.ArtistName,
                        //CreatedUtc = entity.CreatedUtc,
                        Rating = entity.Rating,
                        ArtistId = entity.ArtistId
                    };
            }
        }

        // Specify a store and an album. Add the Store into the Album's ICollection of Stores
        // The join table will be updated with the indices from both tables
        public void AlbumAssignAStore(int storeId, int albumId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var album = ctx
                    .Albums
                    .Single(e => e.AlbumId == albumId);
                var store = ctx
                    .Stores
                    .Single(e => e.StoreId == storeId);
                store.Albums.Add(album);
                ctx.SaveChanges();
            }
        }

    

          //GetByAlbumId
        public IEnumerable<AlbumDetails> GetAlbumByArtist(int ArtistId)

        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Albums
                        .Where(e => e.Artist.ArtistId == ArtistId)
                        .Select(
                            e =>
                                new AlbumDetails
                                {
                                    
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                    AlbumName = e.AlbumName,
                                    ArtistName = e.Artist.ArtistName,

                                }
                        );

                return query.ToArray();
            }
        }



        public bool UpdateAlbum(AlbumEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Albums
                        .Single(e => e.AlbumId == model.AlbumId && e.OwnerId == _userId);


                entity.AlbumName = model.AlbumName;
                entity.Rating = model.Rating;
                entity.CulumativeRating = model.CulumativeRating;
                entity.NumberOfRatings = model.NumberOfRatings;
                entity.ArtistId = model.ArtistId;

                return ctx.SaveChanges() == 1;
            }
        }
        //public bool UpdateRatingAverage(AlbumEdit model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Albums
        //                .Single(e => e.AlbumId == model.AlbumId);
        //        entity.Rating = model.Rating;
        //        entity.CulumativeRating = model.CulumativeRating;
        //        entity.NumberOfRatings = model.NumberOfRatings;

        //        return ctx.SaveChanges() == 1;

        //    } //Update the rating only 
        //}
        public bool DeleteAlbum(int albumId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Albums
                        .Single(e => e.AlbumId == albumId && e.OwnerId == _userId);

                ctx.Albums.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}



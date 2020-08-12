using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class StoreService
    {
        private readonly Guid _userId;

        public StoreService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateStore(StoreCreate model)
        {
            var entity =
                new Store()
                {
                    OwnerId = _userId,
                    StoreId = model.StoreId,
                    StoreName = model.StoreName,
                    Address = model.Address,
                    StoreRating  =model.StoreRating
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Stores.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //----------------------------------
        public IEnumerable<StoreListItem> GetStores()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Stores
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new StoreListItem
                                {
                                    StoreId = e.StoreId,
                                    StoreName = e.StoreName,
                                    Address = e.Address,
                                    StoreRating = e.StoreRating,
                                }
                        );

                return query.ToArray();
            }
        }

        public StoreDetail GetStoreById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == id && e.OwnerId == _userId);
                return
                    new StoreDetail
                    {
                        StoreId = entity.StoreId,
                        StoreName = entity.StoreName,
                        Address = entity.Address,
                        StoreRating = entity.StoreRating
                    };
            }
        }

        public List<AlbumDetails> GetAllAlbumsWithStore(int storeId, bool getAlbums)
        {
            if (getAlbums)
            {
                // You're in the right place
            }
            using (var ctx = new ApplicationDbContext())
            {
                var store =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == storeId);
                List<AlbumDetails> albumList = new List<AlbumDetails>();
                var albums = store.Albums.ToArray();
                foreach (var album in albums)
                {
                    AlbumDetails listItem = new AlbumDetails();
                    listItem.AlbumId = album.AlbumId;
                    listItem.AlbumName = album.AlbumName;
                    listItem.ArtistName = album.Artist.ArtistName;
                    listItem.Rating = album.Rating;

                    albumList.Add(listItem);
                }
                return albumList;
            }
        }


        public bool UpdateStore(StoreEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == model.StoreId && e.OwnerId == _userId);

                entity.StoreName = model.StoreName;
                entity.Address = model.Address;
                entity.StoreRating = model.StoreRating;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteStore(int noteId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Stores
                        .Single(e => e.StoreId == noteId && e.OwnerId == _userId);

                ctx.Stores.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

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

            public bool CreateAlbum(AlbumCreate model)
            {
            var entity =
                new Album()
                {
                        OwnerId = _userId,
                        AlbumName = model.AlbumName,
                        Rating = model.Rating,
                        CreatedUtc = DateTimeOffset.Now
                    };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Albums.Add(entity);
                    return ctx.SaveChanges() == 1;
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
                                    Rating = e.Rating,
                                    CreatedUtc = e.CreatedUtc
                                }
                                );
                    return query.ToArray();
                }
            }

            //public AlbumDetails GetAlbumById(int id)
            //{
            //    using (var ctx = new ApplicationDbContext())
            //    {
            //        var entity =
            //            ctx
            //                .Albums
            //                .Single(e => e.AlbumId == id && e.OwnerId == _userId);
            //        return
            //            new AlbumDetails
            //            {
            //                AlbumId = entity.AlbumId,
            //                AlbumName = entity.AlbumName,
            //                CreatedUtc = entity.CreatedUtc,
            //                Rating = entity.Rating
            //            };
            //    }
            //}

            //public bool DeleteAlbum(int noteId)
            //{
            //    using (var ctx = new ApplicationDbContext())
            //    {
            //        var entity =
            //            ctx
            //                .Albums
            //                .Single(e => e.AlbumId == noteId && e.OwnerId == _userId);

            //        ctx.Albums.Remove(entity);

            //        return ctx.SaveChanges() == 1;
            //    }
            //}


        }
    }


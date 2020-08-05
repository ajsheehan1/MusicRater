using MusicRater.Data;
using MusicRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Services
{
    public class SongService
    {
        private readonly Guid _userId;

        public SongService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSong(SongCreate model)
        {
            var entity =
                new Song()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Rating = model.Rating,
                    AlbumId = model.AlbumId,
                    // Adding Album/Artist Foreign Key Here
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Songs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SongListItem> GetSongs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SongListItem
                                {
                                    SongId = e.SongId,
                                    Title = e.Title,
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                    // Add Album/Artist foreign key here
                                }
                        );

                return query.ToArray();
            }
        }

        public SongDetail GetSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == id && e.OwnerId == _userId);
                return
                    new SongDetail
                    {
                        SongId = entity.SongId,
                        Title = entity.Title,
                        Rating = entity.Rating,
                        AlbumId = entity.AlbumId,
                        //ForeignKey Here
                    };
            }
        }

        public bool UpdateSong(SongEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs

                        .Single(e => e.SongId == model.SongId && e.OwnerId == _userId);
                    
                entity.Title = model.Title;
                entity.Rating = model.Rating;
                entity.AlbumId= model.AlbumId;
                //ForeignKeyHere

                return ctx.SaveChanges() == 1;

            }
        }
        public bool DeleteSong(int songId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == songId && e.OwnerId == _userId);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

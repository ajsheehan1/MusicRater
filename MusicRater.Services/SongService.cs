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
        public SongService()
        {
        }

        public bool CreateSong(SongCreate model)
        {
            var entity =
                new Song()
                {
                    OwnerId = _userId,
                    SongName = model.SongName,
                    AlbumId = model.AlbumId,
                    
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
                        //.Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new SongListItem
                                {
                                    SongId = e.SongId,
                                    SongName = e.SongName,
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                    
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
                        .Single(e => e.SongId == id);
                        
                return
                    new SongDetail
                    {
                        SongId = entity.SongId,
                        SongName = entity.SongName,
                        Rating = entity.Rating,
                        AlbumId = entity.AlbumId,
                        CulumativeRating = entity.CulumativeRating,
                        NumberOfRatings = entity.NumberOfRatings
                        
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
                entity.SongName = model.SongName;
                entity.Rating = model.Rating;
                entity.CulumativeRating = model.CulumativeRating;
                entity.NumberOfRatings = model.NumberOfRatings;
                entity.AlbumId= model.AlbumId;
                

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

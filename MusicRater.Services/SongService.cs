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

        //CREATE
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

        //Create AlbumName
        //This method assigns the album name upon creation of Song.WORKING
        //public string AlbumName(SongCreate model)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var newSong =
        //            ctx
        //                .Albums
        //                .Single(e => e.AlbumId == model.AlbumId && e.OwnerId == _userId);

        //        string albumName = newSong.AlbumName;
        //        return albumName;
        //    }

        //}


        //GET
        public IEnumerable<SongListItem> GetSongs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Select(
                            e =>
                                new SongListItem
                                {
                                    SongId = e.SongId,
                                    SongName = e.SongName,
                                    AlbumName = e.Album.AlbumName,
                                    ArtistName = e.Album.Artist.ArtistName,
                                    //AlbumName = GetAlbumName(e.AlbumId).ToString(),
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                }
                        );

                return query.ToArray();
            }
        }
        //GetbyID
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
                        AlbumName = entity.Album.AlbumName,
                        ArtistName = entity.Album.Artist.ArtistName,
                        //AlbumName = ListAlbumName(entity.AlbumId),
                        //CulumativeRating = entity.CulumativeRating,
                        NumberOfRatings = entity.NumberOfRatings

                    };
            }
        }


        //GetByAlbumID
        public IEnumerable<SongDetail> GetSongsByAlbum(int AlbumId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Where(e => e.AlbumId == AlbumId)
                        .Select(
                            e =>
                                new SongDetail
                                {
                                    SongId = e.SongId,
                                    SongName = e.SongName,
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                    AlbumName = e.Album.AlbumName,
                                    ArtistName = e.Album.Artist.ArtistName,


                                }
                        );

                return query.ToArray();
            }
        }


        //GetByArtistId
        public IEnumerable<SongDetail> GetSongsByArtist(int ArtistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Songs
                        .Where(e => e.Album.Artist.ArtistId == ArtistId)
                        .Select(
                            e =>
                                new SongDetail
                                {
                                    SongId = e.SongId,
                                    SongName = e.SongName,
                                    Rating = e.Rating,
                                    AlbumId = e.AlbumId,
                                    AlbumName = e.Album.AlbumName,
                                    ArtistName = e.Album.Artist.ArtistName,


                                }
                        );

                return query.ToArray();
            }
        }


        //UPDATE
        public bool UpdateSong(SongEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Songs
                        .Single(e => e.SongId == model.SongId);
                entity.SongName = model.SongName;
                entity.Rating = model.Rating;
                entity.CulumativeRating = model.CulumativeRating;
                entity.NumberOfRatings = model.NumberOfRatings;
                entity.AlbumId= model.AlbumId;
                //entity.AlbumName = AlbumNameUpdate(model);
                return ctx.SaveChanges() == 1;

            }
        }

        
        //DELETE
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

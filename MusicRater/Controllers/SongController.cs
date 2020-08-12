using Microsoft.AspNet.Identity;
using MusicRater.Models;
using MusicRater.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicRater.Controllers
{

    [Authorize]
    public class SongController : ApiController
    {
        /// <summary>
        /// Returns a list of all Songs
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            SongService songService = CreateSongService();
            var songs = songService.GetSongs();
            return Ok(songs);
        }
        /// <summary>
        /// Creates a new Song
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public IHttpActionResult Post(SongCreate song)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.CreateSong(song))
                return InternalServerError();

            return Ok();
        }
        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var songService = new SongService(userId);
            return songService;
        }
        /// <summary>
        /// Returns a single Song by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            SongService songService = CreateSongService();
            var note = songService.GetSongById(id);
            return Ok(note);
        }
        /// <summary>
        /// Returns Songs on an Album
        /// </summary>
        /// <param name="AlbumId"></param>
        /// <returns></returns>
        public IHttpActionResult GetByAlbum(int AlbumId)
        {
            SongService songService = CreateSongService();
            var note = songService.GetSongsByAlbum(AlbumId);
            return Ok(note);
        }

        /// <summary>
        /// Returns Songs by an Artist
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public IHttpActionResult GetByArtist(int ArtistId)
        {
            SongService songService = CreateSongService();
            var note = songService.GetSongsByArtist(ArtistId);
            return Ok(note);
        }

        /// <summary>
        /// Updates a Song
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public IHttpActionResult Put(SongEdit song)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.UpdateSong(song))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// Deletes a Song by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSongService();

            if (!service.DeleteSong(id))
                return InternalServerError();

            return Ok();
        }
    }

   
}

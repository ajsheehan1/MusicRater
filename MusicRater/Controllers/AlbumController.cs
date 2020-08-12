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
    public class AlbumController : ApiController
    {
        /// <summary>
        /// Returns a list of all Albums
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            AlbumService albumService = CreateAlbumService();
            var albums = albumService.GetAlbums();
            return Ok(albums);
        }
        /// <summary>
        /// Creates a new Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        //public IHttpActionResult Get(int)
        public IHttpActionResult Post(AlbumCreate album)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumService();

            if (!service.CreateAlbum(album))
                return InternalServerError();

            return Ok();
        }

        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var AlbumService = new AlbumService(userId);
            return AlbumService;
        }
        /// <summary>
        /// Returns an Album by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            AlbumService albumService = CreateAlbumService();
            var note = albumService.GetAlbumById(id);
            return Ok(note);
        } // Get by ID

        /// <summary>
        /// Returns an Artist by Id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <returns></returns>
        public IHttpActionResult GetByArtist(int ArtistId)
        {
            AlbumService albumService = CreateAlbumService();
            var note = albumService.GetSongsByArtist(ArtistId);
            return Ok(note);
        }

        /// <summary>
        /// Updates an Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        public IHttpActionResult Put(AlbumEdit album)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumService();

            if (!service.UpdateAlbum(album))
                return InternalServerError();

            return Ok();
        }
        /// <summary>
        /// Deletes an Album by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateAlbumService();

            if (!service.DeleteAlbum(id))
                return InternalServerError();

            return Ok();
        }
    }
}

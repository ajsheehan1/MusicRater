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

        
        private AlbumService CreateAlbumService()

        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var AlbumService = new AlbumService(userId);
            return AlbumService;
        }


        // Create a new Album from the contents of the body

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

        // Assign Album albumId to Store storeId - arguments in the Uri
        /// <summary>
        /// Adds an association between an Album & Store
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="albumId"></param>
        /// <returns></returns>
        public IHttpActionResult Post(int storeId, int albumId)
        {
            var service = CreateAlbumService();

            service.AlbumAssignAStore(storeId, albumId);

            return Ok();
        }
        /// <summary>
        /// Returns a list of Albums
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            AlbumService albumService = CreateAlbumService();
            var albums = albumService.GetAlbums();
            return Ok(albums);
        }

        /// <summary>
        /// Returns a list of Stores that has an Album
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="getStores"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int albumId, bool getStores)
        {
            AlbumService albumService = CreateAlbumService();
            var stores = albumService.GetAllStoresWithAlbum(albumId, getStores);
            return Ok(stores);
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
            var note = albumService.GetAlbumByArtist(ArtistId);
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

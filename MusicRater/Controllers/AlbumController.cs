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
        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var AlbumService = new AlbumService(userId);
            return AlbumService;
        }

        // Create a new Album from the contents of the body
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
        public IHttpActionResult Post(int storeId, int albumId)
        {
            var service = CreateAlbumService();

            service.AlbumAssignAStore(storeId, albumId);

            return Ok();
        }

        public IHttpActionResult Get()
        {
            AlbumService albumService = CreateAlbumService();
            var albums = albumService.GetAlbums();
            return Ok(albums);
        }

        public IHttpActionResult Get(int id)
        {
            AlbumService albumService = CreateAlbumService();
            var albums = albumService.GetAlbumById(id);
            return Ok(albums);
        }

        public IHttpActionResult Get(int albumId, bool getStores)
        {
            AlbumService albumService = CreateAlbumService();
            var stores = albumService.GetAllStoresWithAlbum(albumId, getStores);
            return Ok(stores);
        }

        public IHttpActionResult Put(AlbumEdit album)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumService();

            if (!service.UpdateAlbum(album))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateAlbumService();

            if (!service.DeleteAlbum(id))
                return InternalServerError();

            return Ok();
        }
    }
}

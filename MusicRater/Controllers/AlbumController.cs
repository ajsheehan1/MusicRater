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

        public IHttpActionResult Get()
        {
            AlbumService albumService = CreateAlbumService();
            var albums = albumService.GetAlbums();
            return Ok(albums);
        }
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
    }
}

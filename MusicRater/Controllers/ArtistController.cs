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
    public class ArtistController : ApiController
    {
        public IHttpActionResult Get()
        {
            ArtistService artistService = CreateArtistService();
            var artists = artistService.GetArtists();
            return Ok(artists);
        } // Get

        public IHttpActionResult Post(ArtistCreate artist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistService();

            if (!service.CreateArtist(artist))
                return InternalServerError();

            return Ok();
        } // Post

        private ArtistService CreateArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var artistService = new ArtistService(userId);
            return artistService;
        } // CreateArtistService

    }
}

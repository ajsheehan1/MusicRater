using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MusicRater.Models;
using MusicRater.Services;

namespace MusicRater.Controllers
{
    public class AlbumRatingController : ApiController
    {
        public IHttpActionResult Get()
        {
            AlbumRatingService albumRatingService = CreateAlbumRatingService();
            var albumRatings = albumRatingService.GetAlbumRatings();
            return Ok(albumRatings);
        } // Get

        public IHttpActionResult Get(int id)
        {
            AlbumRatingService albumRatingService = CreateAlbumRatingService();
            var albumRatings = albumRatingService.GetRatingsByAlbum(id);
            return Ok(albumRatings);
        } // Get by ID




        public IHttpActionResult Post(AlbumRatingCreate albumRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumRatingService();

            if (!service.CreateAlbumRating(albumRating))
                return InternalServerError();

            return Ok();
        } // Post

        private AlbumRatingService CreateAlbumRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var albumRatingService = new AlbumRatingService(userId);
            return albumRatingService;
        } // CreateAlbumRatingService

        public IHttpActionResult Put(AlbumRatingEdit albumRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumRatingService();

            if (!service.UpdateAlbumRating(albumRating))
                return InternalServerError();

            return Ok();
        } // Put
        public IHttpActionResult Delete(int id)
        {
            var service = CreateAlbumRatingService();

            if (!service.DeleteAlbumRating(id))
                return InternalServerError();

            return Ok();
        } // Delete
    }
}

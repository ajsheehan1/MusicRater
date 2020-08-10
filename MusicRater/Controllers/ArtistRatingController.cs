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
    public class ArtistRatingController : ApiController
    {
        public IHttpActionResult Get()
        {
            ArtistRatingService artistRatingService = CreateArtistRatingService();
            var artistRatings = artistRatingService.GetArtistRatings();
            return Ok(artistRatings);
        } // Get

        public IHttpActionResult Get(int id)
        {
            ArtistRatingService artistRatingService = CreateArtistRatingService();
            var artistRatings = artistRatingService.GetRatingsByArtist(id);
            return Ok(artistRatings);
        } // Get by ID

        public IHttpActionResult Post(ArtistRatingCreate artistRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistRatingService();

            if (!service.CreateArtistRating(artistRating))
                return InternalServerError();

            return Ok();
        } // Post

        private ArtistRatingService CreateArtistRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var artistRatingService = new ArtistRatingService(userId);
            return artistRatingService;
        } // CreateArtistRatingService

        public IHttpActionResult Put(ArtistRatingEdit artistRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistRatingService();

            if (!service.UpdateArtistRating(artistRating))
                return InternalServerError();

            return Ok();
        } // Put
        public IHttpActionResult Delete(int id)
        {
            var service = CreateArtistRatingService();

            if (!service.DeleteArtistRating(id))
            {
                return InternalServerError();
            }
            return Ok();
        } // Delete

    }
}

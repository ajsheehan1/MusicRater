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
        /// <summary>
        /// Returns a Rating of All Artist
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            ArtistRatingService artistRatingService = CreateArtistRatingService();
            var artistRatings = artistRatingService.GetArtistRatings();
            return Ok(artistRatings);
        } // Get
        /// <summary>
        /// Returns a Rating of a single Artist by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            ArtistRatingService artistRatingService = CreateArtistRatingService();
            var artistRatings = artistRatingService.GetRatingsByArtist(id);
            return Ok(artistRatings);
        } // Get by ID
        /// <summary>
        /// Adds an Artist Rating
        /// </summary>
        /// <param name="artistRating"></param>
        /// <returns></returns>
        public IHttpActionResult Post(ArtistRatingCreate artistRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistRatingService();

            if (!service.CreateArtistRating(artistRating))
                return InternalServerError();

            return Ok();
        } // Post
        /// <summary>
        /// Creates a Artist Rating
        /// </summary>
        /// <returns></returns>
        private ArtistRatingService CreateArtistRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var artistRatingService = new ArtistRatingService(userId);
            return artistRatingService;
        } // CreateArtistRatingService
        /// <summary>
        /// Edits an Artist Rating
        /// </summary>
        /// <param name="artistRating"></param>
        /// <returns></returns>
        public IHttpActionResult Put(ArtistRatingEdit artistRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistRatingService();

            if (!service.UpdateArtistRating(artistRating))
                return InternalServerError();

            return Ok();
        } // Put
        /// <summary>
        /// Deletes an Artist Rating by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

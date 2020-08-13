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
    public class SongRatingController : ApiController
    {
        /// <summary>
        /// Returns the Rating of all Songs
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            SongRatingService songRatingService = CreateSongRatingService();
            var songRatings = songRatingService.GetSongRatings();
            return Ok(songRatings);
        } // Get
        /// <summary>
        /// Returns the Rating of a single Song by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            SongRatingService songRatingService = CreateSongRatingService();
            var songRatings = songRatingService.GetRatingsBySong(id);
            return Ok(songRatings);
        } // Get by ID
        /// <summary>
        /// Adds a Song Rating
        /// </summary>
        /// <param name="songRating"></param>
        /// <returns></returns>
        public IHttpActionResult Post(SongRatingCreate songRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongRatingService();

            if (!service.CreateSongRating(songRating))
                return InternalServerError();

            return Ok();
        } // Post
        /// <summary>
        /// Creates a Song Rating
        /// </summary>
        /// <returns></returns>
        private SongRatingService CreateSongRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var songRatingService = new SongRatingService(userId);
            return songRatingService;
        } // CreateSongRatingService
        /// <summary>
        /// Edits a Song Rating
        /// </summary>
        /// <param name="songRating"></param>
        /// <returns></returns>
        public IHttpActionResult Put(SongRatingEdit songRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongRatingService();

            if (!service.UpdateSongRating(songRating))
                return InternalServerError();

            return Ok();
        } // Put
        /// <summary>
        /// Deletes a Song Rating by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSongRatingService();

            if (!service.DeleteSongRating(id))
                return InternalServerError();

            return Ok();
        } // Delete

    }
}

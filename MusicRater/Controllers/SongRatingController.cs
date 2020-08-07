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
        public IHttpActionResult Get()
        {
            SongRatingService songRatingService = CreateSongRatingService();
            var songRatings = songRatingService.GetSongRatings();
            return Ok(songRatings);
        } // Get

        public IHttpActionResult Get(int id)
        {
            SongRatingService songRatingService = CreateSongRatingService();
            var songRatings = songRatingService.GetRatingsBySong(id);
            return Ok(songRatings);
        } // Get by ID

        public IHttpActionResult Post(SongRatingCreate songRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongRatingService();

            if (!service.CreateSongRating(songRating))
                return InternalServerError();

            return Ok();
        } // Post

        private SongRatingService CreateSongRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var songRatingService = new SongRatingService(userId);
            return songRatingService;
        } // CreateSongRatingService

        public IHttpActionResult Put(SongRatingEdit songRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongRatingService();

            if (!service.UpdateSongRating(songRating))
                return InternalServerError();

            return Ok();
        } // Put
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSongRatingService();

            if (!service.DeleteSongRating(id))
                return InternalServerError();

            return Ok();
        } // Delete

    }
}

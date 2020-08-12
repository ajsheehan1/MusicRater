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
        /// <summary>
        /// Returns the Rating of All Albums
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            AlbumRatingService albumRatingService = CreateAlbumRatingService();
            var albumRatings = albumRatingService.GetAlbumRatings();
            return Ok(albumRatings);
        } // Get
        /// <summary>
        /// Returns the Rating of a single Album by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            AlbumRatingService albumRatingService = CreateAlbumRatingService();
            var albumRatings = albumRatingService.GetRatingsByAlbum(id);
            return Ok(albumRatings);
        } // Get by ID

        /// <summary>
        /// Adds a new Album Rating
        /// </summary>
        /// <param name="albumRating"></param>
        /// <returns></returns>
        public IHttpActionResult Post(AlbumRatingCreate albumRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumRatingService();

            if (!service.CreateAlbumRating(albumRating))
                return InternalServerError();

            return Ok();
        } // Post
        /// <summary>
        /// Creates a Album Rating
        /// </summary>
        /// <returns></returns>
        private AlbumRatingService CreateAlbumRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var albumRatingService = new AlbumRatingService(userId);
            return albumRatingService;
        } // CreateAlbumRatingService
        /// <summary>
        /// Edits a Album Rating
        /// </summary>
        /// <param name="albumRating"></param>
        /// <returns></returns>
        public IHttpActionResult Put(AlbumRatingEdit albumRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateAlbumRatingService();

            if (!service.UpdateAlbumRating(albumRating))
                return InternalServerError();

            return Ok();
        } // Put
        /// <summary>
        /// Deletes a Album Rating
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateAlbumRatingService();

            if (!service.DeleteAlbumRating(id))
                return InternalServerError();

            return Ok();
        } // Delete
    }
}

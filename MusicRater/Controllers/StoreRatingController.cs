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
    public class StoreRatingController : ApiController
    {
        // GET: StoreRating
        /// <summary>
        /// Returns the Rating of All Stores
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            StoreRatingService StoreRatingService = CreateStoreRatingService();
            var storeRatings = StoreRatingService.GetStoreRatings();
            return Ok(storeRatings);
        }
        
        // Get
        /// <summary>
        /// Returns the Rating of a single Store by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            StoreRatingService StoreRatingService = CreateStoreRatingService();
            var storeRatings = StoreRatingService.GetRatingsByStore(id);
            return Ok(storeRatings);
        } // Get by ID

        /// <summary>
        /// Adds a Store Rating
        /// </summary>
        /// <param name="storeRating"></param>
        /// <returns></returns>
        public IHttpActionResult Post(StoreRatingCreate storeRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreRatingService();

            if (!service.CreateStoreRating(storeRating))
                return InternalServerError();

            return Ok();
        } // Post

        /// <summary>
        /// Creates a Store Rating
        /// </summary>
        /// <returns></returns>
        private StoreRatingService CreateStoreRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var StoreRatingService = new StoreRatingService(userId);
            return StoreRatingService;
        } // CreateStoreRatingService

        /// <summary>
        /// Edits a Store Rating
        /// </summary>
        /// <param name="storeRating"></param>
        /// <returns></returns>
        public IHttpActionResult Put(StoreRatingEdit storeRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreRatingService();

            if (!service.UpdateStoreRating(storeRating))
                return InternalServerError();

            return Ok();
        } // Put
        /// <summary>
        /// Deletes a Store Rating by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateStoreRatingService();

            if (!service.DeleteStoreRating(id))
            {
                return InternalServerError();
            }
            return Ok();
        } // Delete
    }
}
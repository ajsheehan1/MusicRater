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
        public IHttpActionResult Get()
        {
            StoreRatingService StoreRatingService = CreateStoreRatingService();
            var storeRatings = StoreRatingService.GetStoreRatings();
            return Ok(storeRatings);
        } // Get

        public IHttpActionResult Get(int id)
        {
            StoreRatingService StoreRatingService = CreateStoreRatingService();
            var storeRatings = StoreRatingService.GetRatingsByStore(id);
            return Ok(storeRatings);
        } // Get by ID

        public IHttpActionResult Post(StoreRatingCreate storeRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreRatingService();

            if (!service.CreateStoreRating(storeRating))
                return InternalServerError();

            return Ok();
        } // Post

        private StoreRatingService CreateStoreRatingService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var StoreRatingService = new StoreRatingService(userId);
            return StoreRatingService;
        } // CreateStoreRatingService

        public IHttpActionResult Put(StoreRatingEdit storeRating)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreRatingService();

            if (!service.UpdateStoreRating(storeRating))
                return InternalServerError();

            return Ok();
        } // Put
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
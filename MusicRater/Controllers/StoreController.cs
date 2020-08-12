using Microsoft.AspNet.Identity;
using MusicRater.Data;
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
    public class StoreController : ApiController
    {

        private StoreService CreateStoreService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var storeService = new StoreService(userId);
            return storeService;
        }

        /// <summary>
        /// Returns a list of all Stores
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            StoreService storeService = CreateStoreService();
            var stores = storeService.GetStores();
            return Ok(stores);
        }

        /// <summary>
        /// Creates a new Store
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public IHttpActionResult Post(StoreCreate store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.CreateStore(store))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Returns a single store by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            StoreService storeService = CreateStoreService();
            var store = storeService.GetStoreById(id);
            return Ok(store);
        }

        /// <summary>
        /// Updates info for a  Store
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public IHttpActionResult Put(StoreEdit store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.UpdateStore(store))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Deletes a single Store by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateStoreService();

            if (!service.DeleteStore(id))
                return InternalServerError();

            return Ok();
        }

    }
}


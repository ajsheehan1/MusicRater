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

        public IHttpActionResult Get()
        {
            StoreService storeService = CreateStoreService();
            var stores = storeService.GetStores();
            return Ok(stores);
        }

        public IHttpActionResult Post(StoreCreate store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.CreateStore(store))
                return InternalServerError();

            return Ok();
        }


        public IHttpActionResult Get(int id)
        {
            StoreService storeService = CreateStoreService();
            var note = storeService.GetStoreById(id);
            return Ok(note);
        }

        public IHttpActionResult Put(StoreEdit store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateStoreService();

            if (!service.UpdateStore(store))
                return InternalServerError();

            return Ok();
        }

    }
}


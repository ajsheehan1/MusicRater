using Microsoft.AspNet.Identity;
using MusicRater.Models;
using MusicRater.Services;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace MusicRater.Controllers
{
    [System.Web.Http.Authorize]
    public class ArtistController : ApiController
    {
        private ArtistService CreateArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var artistService = new ArtistService(userId);
            return artistService;
        } // CreateArtistService

        /// <summary>
        /// Returns a list of all Artist
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            ArtistService artistService = CreateArtistService();
            var artists = artistService.GetArtists();
            return Ok(artists);
        } // Get
        /// <summary>
        /// Returns a single Artist by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            ArtistService artistService = CreateArtistService();
            var artists = artistService.GetArtistById(id);
            return Ok(artists);
        } // Get by ID
        /// <summary>
        /// Creates a new Artist
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public IHttpActionResult Post(ArtistCreate artist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistService();

            if (!service.CreateArtist(artist))
                return InternalServerError();

            return Ok();
        } // Post

        /// <summary>
        /// Returns a single Artist by Id
        /// </summary>
        /// <returns></returns>
         // CreateArtistService
        /// <summary>
        /// Updates info for an Artist
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public IHttpActionResult Put(ArtistEdit artist)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateArtistService();

            if (!service.UpdateArtist(artist))
                return InternalServerError();

            return Ok();
        } // Put
        /// <summary>
        /// Deletes an Artist by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            var service = CreateArtistService();

            if (!service.DeleteArtist(id))
                return InternalServerError();

            return Ok();
        } // Delete

        //public ActionResult Artist()
        //{
        //    ViewBag.Title = "Artist";

        //    return View();
        //}


    }


}

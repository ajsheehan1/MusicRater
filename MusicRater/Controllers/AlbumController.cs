﻿using Microsoft.AspNet.Identity;
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
    public class AlbumController : ApiController
    {
        public IHttpActionResult Get()
        {
            AlbumService albumService = CreateNoteService();
            var notes = albumService.GetAlbums();
            return Ok(notes);
        }
        public IHttpActionResult Post(AlbumCreate album)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateNoteService();

            if (!service.CreateAlbum(album))
                return InternalServerError();

            return Ok();
        }
        private AlbumService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new AlbumService(userId);
            return noteService;
        }
    }
}
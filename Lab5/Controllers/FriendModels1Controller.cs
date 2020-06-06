using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Lab5.Models;

namespace Lab5.Controllers
{
    public class FriendModels1Controller : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FriendModels1
        public List<FriendDto> GetFriends()
        {
            return db.Friends.Select(x => new FriendDto
            {
                Id = x.Id,
                idFriend = x.idFriend,
                ime = x.ime,
                mestoZiveenje = x.mestoZiveenje
            }).ToList();
        }

        // GET: api/FriendModels1/5
        [ResponseType(typeof(FriendModel))]
        public IHttpActionResult GetFriendModel(int id)
        {
            FriendModel friendModel = db.Friends.Find(id);
            if (friendModel == null)
            {
                return NotFound();
            }

            return Ok(friendModel);
        }

        // PUT: api/FriendModels1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFriendModel(int id, FriendModel friendModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendModel.Id)
            {
                return BadRequest();
            }

            db.Entry(friendModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FriendModels1
        [ResponseType(typeof(FriendModel))]
        public IHttpActionResult PostFriendModel(FriendModel friendModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friends.Add(friendModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = friendModel.Id }, friendModel);
        }

        // DELETE: api/FriendModels1/5
        [ResponseType(typeof(FriendModel))]
        public IHttpActionResult DeleteFriendModel(int id)
        {
            FriendModel friendModel = db.Friends.Find(id);
            if (friendModel == null)
            {
                return NotFound();
            }

            db.Friends.Remove(friendModel);
            db.SaveChanges();

            return Ok(friendModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendModelExists(int id)
        {
            return db.Friends.Count(e => e.Id == id) > 0;
        }
    }
}
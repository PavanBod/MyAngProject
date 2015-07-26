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
using CIT.DAL;
using CIT.Models;

namespace CIT.Web.Services.Controllers
{
    public class PhrasesController : ApiController
    {
        public CITDataContext CITDataContext { get { return new CITDataContext(); } }

        // GET: api/Phrases
        public IQueryable<Post> GetPosts()
        {
            return CITDataContext.Posts;
        }

        // GET: api/Phrases/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetPost(int id)
        {
            Post post = CITDataContext.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/Phrases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPost(int id, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.Id)
            {
                return BadRequest();
            }

            CITDataContext.Entry(post).State = EntityState.Modified;

            try
            {
                CITDataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Phrases
        [ResponseType(typeof(Post))]
        public IHttpActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CITDataContext.Posts.Add(post);
            CITDataContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, post);
        }

        // DELETE: api/Phrases/5
        [ResponseType(typeof(Post))]
        public IHttpActionResult DeletePost(int id)
        {
            Post post = CITDataContext.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            CITDataContext.Posts.Remove(post);
            CITDataContext.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CITDataContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostExists(int id)
        {
            return CITDataContext.Posts.Count(e => e.Id == id) > 0;
        }
    }
}
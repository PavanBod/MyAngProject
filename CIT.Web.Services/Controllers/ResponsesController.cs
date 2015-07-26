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
    public class ResponsesController : ApiController
    {
        public CITDataContext CITDataContext { get { return new CITDataContext(); } }

        // GET: api/Responses
        public IQueryable<Response> GetResponses()
        {
            return CITDataContext.Responses;
        }

        // GET: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult GetResponse(int id)
        {
            Response response = CITDataContext.Responses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // PUT: api/Responses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponse(int id, Response response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != response.Id)
            {
                return BadRequest();
            }

            CITDataContext.Entry(response).State = EntityState.Modified;

            try
            {
                CITDataContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponseExists(id))
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

        // POST: api/Responses
        [ResponseType(typeof(Response))]
        public IHttpActionResult PostResponse(Response response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CITDataContext.Responses.Add(response);
            CITDataContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = response.Id }, response);
        }

        // DELETE: api/Responses/5
        [ResponseType(typeof(Response))]
        public IHttpActionResult DeleteResponse(int id)
        {
            Response response = CITDataContext.Responses.Find(id);
            if (response == null)
            {
                return NotFound();
            }

            CITDataContext.Responses.Remove(response);
            CITDataContext.SaveChanges();

            return Ok(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CITDataContext.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResponseExists(int id)
        {
            return CITDataContext.Responses.Count(e => e.Id == id) > 0;
        }
    }
}
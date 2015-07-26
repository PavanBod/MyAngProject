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
using System.Net.Http.Formatting;
using CIT.Common;
using System.Web.Http.Cors;

namespace CIT.Web.Services.Controllers
{
    [RoutePrefix("api/MySiteUsers")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MySiteUsersController : ApiController
    {
        private CITDataContext db = new CITDataContext();

        // GET: api/MySiteUsers
        public IQueryable<UserInfo> GetUsers()
        {
            return db.Users;
        }

        // GET: api/MySiteUsers/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult GetUserInfo(string id)
        {
            UserInfo userInfo = db.Users.Find(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }

        // PUT: api/MySiteUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserInfo(string id, UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userInfo.UserId)
            {
                return BadRequest();
            }

            db.Entry(userInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(id))
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

        // POST: api/MySiteUsers
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult PostUserInfo(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userInfo.Password = userInfo.Password.ToCryptoStringAES(CITConstants.CRYPTO_AES_KEY.GetStringFromByteArray(), CITConstants.CRYPTI_AES_IV.GetStringFromByteArray());
            db.Users.Add(userInfo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserInfoExists(userInfo.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userInfo.UserId }, userInfo);
        }

        [HttpPost, Route("AuthenticateUser")]
        [ActionName("AuthenticateUser")]
        public IHttpActionResult AuthenticateUser(FormDataCollection userDetails)
        {
            bool isAuthenticated = false;

            var userId = userDetails.Get("userId");
            var password = userDetails.Get("password");

            var passwordEncrypted = password.ToCryptoStringAES(CITConstants.CRYPTO_AES_KEY.GetStringFromByteArray(), CITConstants.CRYPTI_AES_IV.GetStringFromByteArray());

            isAuthenticated = db.Users.Where(u => u.UserId.Equals(userId) && u.Password.Equals(passwordEncrypted)).ToList().Count > 0;

            if (isAuthenticated)
                return Ok("success");
            else
                return Unauthorized();
        }

        // DELETE: api/MySiteUsers/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult DeleteUserInfo(string id)
        {
            UserInfo userInfo = db.Users.Find(id);
            if (userInfo == null)
            {
                return NotFound();
            }

            db.Users.Remove(userInfo);
            db.SaveChanges();

            return Ok(userInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserInfoExists(string id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}
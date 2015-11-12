using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        /// <summary>
        ///     Get all users
        /// </summary>
        /// <remarks>
        ///     Get an array of all users
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof (List<User>))]
        public IHttpActionResult Get()
        {
            var context = new CamarillaContext();
            var store = new UserStore(context);

            var userInformations = store.Users.ToList();

            return Ok(userInformations);
        }

        /// <summary>
        ///     Get a user from DB
        /// </summary>
        /// <param name="username">
        ///     Email
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("{username}")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return BadRequest("username cannot be null or white space");

            var context = new CamarillaContext();
            var store = new UserStore(context);
            var manager = new UserManager(store);

            var user = manager.FindByName(username);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        ///     Get a fake user
        /// </summary>
        /// <remarks>
        ///     Get an object of a fake object user
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("sample")]
        [ResponseType(typeof (User))]
        public IHttpActionResult GetSample()
        {
            var userInformation = new User
            {
                FirstName = "Philippe",
                LastName = "Matray",
                Birthday = new DateTime(1988, 8, 1),
                Gender = Gender.Male
            };

            return Ok(userInformation);
        }

        /// <summary>
        ///     Get a fake user from DB
        /// </summary>
        /// <remarks>
        ///     Get a fake object that represents a user from database
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("sampledb")]
        [ResponseType(typeof (User))]
        public IHttpActionResult GetSampleFromDb()
        {
            var context = new CamarillaContext();
            var store = new UserStore(context);
            var manager = new UserManager(store);

            var user = manager.FindByEmail("phmatray@gmail.com");

            return Ok(user);
        }
    }
}
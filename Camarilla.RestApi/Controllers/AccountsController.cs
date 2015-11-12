using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
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
        [Route("users")]
        [ResponseType(typeof (List<UserReturnModel>))]
        public IHttpActionResult GetUsers()
        {
            var userReturnModels = TheUserManager.Users.ToList()
                .Select(user => TheModelFactory.Create(user));

            return Ok(userReturnModels);
        }

        /// <summary>
        ///     Get a user
        /// </summary>
        /// <param name="username">
        ///     Email
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await TheUserManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();
        }

        /// <summary>
        ///     Get a user
        /// </summary>
        /// <param name="username">
        ///     Email
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("user/{username}")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await TheUserManager.FindByNameAsync(username);

            if (user != null)
                return Ok(TheModelFactory.Create(user));

            return NotFound();
        }

        [HttpPost]
        [Route("create")]
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                JoinDate = DateTime.Now
            };

            var addUserResult = await TheUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
                return GetErrorResult(addUserResult);

            var locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }


        ///// <summary>
        /////     Get a fake user
        ///// </summary>
        ///// <remarks>
        /////     Get an object of a fake object user
        ///// </remarks>
        ///// <response code="200">OK</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpGet]
        //[Route("sample")]
        //[ResponseType(typeof (User))]
        //public IHttpActionResult GetSample()
        //{
        //    var userInformation = new User
        //    {
        //        FirstName = "Philippe",
        //        LastName = "Matray",
        //        Birthday = new DateTime(1988, 8, 1),
        //        Gender = Gender.Male
        //    };

        //    return Ok(userInformation);
        //}

        ///// <summary>
        /////     Get a fake user from DB
        ///// </summary>
        ///// <remarks>
        /////     Get a fake object that represents a user from database
        ///// </remarks>
        ///// <response code="200">OK</response>
        ///// <response code="500">Internal Server Error</response>
        //[HttpGet]
        //[Route("sampledb")]
        //[ResponseType(typeof (User))]
        //public IHttpActionResult GetSampleFromDb()
        //{
        //    var context = new CamarillaContext();
        //    var store = new UserStore(context);
        //    var manager = new UserManager(store);

        //    var user = manager.FindByEmail("phmatray@gmail.com");

        //    return Ok(user);
        //}
    }
}
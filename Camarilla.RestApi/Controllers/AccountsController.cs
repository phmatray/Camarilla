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
        ///     Get an array of all users.
        /// </remarks>
        /// <response code="200">OK</response>
        [Route("users")]
        [HttpGet]
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
        /// <param name="id">
        ///     ID
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [Route("user/{id:guid}", Name = "GetUserById")]
        [HttpGet]
        [ResponseType(typeof(UserReturnModel))]
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
        ///     Username
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [Route("user/{username}")]
        [HttpGet]
        [ResponseType(typeof(UserReturnModel))]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await TheUserManager.FindByNameAsync(username);

            if (user != null)
                return Ok(TheModelFactory.Create(user));

            return NotFound();
        }

        /// <summary>
        ///     Create a user
        /// </summary>
        /// <param name="createUserModel">
        ///     CreateUserModel
        /// </param>
        /// <remarks>
        ///     Create a user in the database.
        /// </remarks>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("create")]
        [ResponseType(typeof(UserReturnModel))]
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
    }
}
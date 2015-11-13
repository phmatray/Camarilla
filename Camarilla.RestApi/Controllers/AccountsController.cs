using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Models;
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        /// <param name="id">
        ///     ID
        /// </param>
        /// <remarks>
        ///     Get a user from database by its username.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
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
        ///     Delete a user
        /// </summary>
        /// <param name="id">
        ///     ID
        /// </param>
        /// <remarks>
        ///     Delete a user from database by its username.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await TheUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await TheUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("user/{username}")]
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
        [AllowAnonymous]
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

            var token = await TheUserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, token = token }));
            await TheUserManager.SendEmailAsync(user.Id, "Confirm your account",
                $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>");

            var locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }

        /// <summary>
        ///     Confirm email address
        /// </summary>
        /// <param name="userId">
        ///     Username
        /// </param>
        /// <param name="token">
        ///     Token provided by email
        /// </param>
        /// <remarks>
        ///     Confirm the email address of a user.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [AllowAnonymous]
        [Route("confirmEmail", Name = "ConfirmEmailRoute")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string token = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("", "User Id and Token are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await TheUserManager.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        /// <summary>
        ///     Confirm email address
        /// </summary>
        /// <param name="userId">
        ///     Username
        /// </param>
        /// <param name="password">
        ///     User password
        /// </param>
        /// <param name="token">
        ///     Token provided by email
        /// </param>
        /// <remarks>
        ///     Confirm the email address of a user.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("confirmEmail", Name = "ConfirmEmailRouteWithPassword")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ConfirmEmailWithPassword(string userId = "", string password = "", string token = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            {
                ModelState.AddModelError("", "User Id and Token are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await TheUserManager.ConfirmEmailAsync(userId, token);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        /// <summary>
        ///     Change password
        /// </summary>
        /// <param name="model">
        ///     ChangePasswordBindingModel
        /// </param>
        /// <remarks>
        ///     Change the password of the current user.
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Identity.GetUserId();
            var result = await TheUserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }



        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await TheUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await TheUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await TheUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await TheUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/assignclaims")]
        public async Task<IHttpActionResult> AssignClaimsToUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToAssign)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await TheUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            foreach (ClaimBindingModel claimModel in claimsToAssign)
            {
                if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
                {

                    await TheUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
                }

                await TheUserManager.AddClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
            }

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/removeclaims")]
        [HttpPut]
        public async Task<IHttpActionResult> RemoveClaimsFromUser([FromUri] string id, [FromBody] List<ClaimBindingModel> claimsToRemove)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = await TheUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            foreach (ClaimBindingModel claimModel in claimsToRemove)
            {
                if (appUser.Claims.Any(c => c.ClaimType == claimModel.Type))
                {
                    await TheUserManager.RemoveClaimAsync(id, ExtendedClaimsProvider.CreateClaim(claimModel.Type, claimModel.Value));
                }
            }

            return Ok();
        }
    }
}
using System;
using System.Threading.Tasks;
using System.Web.Http;
using Camarilla.RestApi.Controllers.ControllerModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Camarilla.RestApi.Controllers
{
    /// <summary>
    ///     Roles controller
    /// </summary>
    //[Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseApiController
    {
        /// <summary>
        ///     Get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);

            if (role != null)
                return Ok(TheModelFactory.Create(role));

            return NotFound();
        }

        /// <summary>
        ///     Get all roles
        /// </summary>
        /// <returns></returns>
        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            var roles = AppRoleManager.Roles;

            return Ok(roles);
        }

        /// <summary>
        ///     Create a role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var role = new IdentityRole {Name = model.Name};
            var result = await AppRoleManager.CreateAsync(role);

            if (!result.Succeeded)
                return GetErrorResult(result);

            var locationHeader = new Uri(Url.Link("GetRoleById", new {id = role.Id}));

            return Created(locationHeader, TheModelFactory.Create(role));
        }

        /// <summary>
        ///     Delete a role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {
            var role = await AppRoleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await AppRoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                    return GetErrorResult(result);

                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        ///     Manage users in role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await AppRoleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role does not exist");
                return BadRequest(ModelState);
            }

            foreach (var user in model.EnrolledUsers)
            {
                var appUser = await TheUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", $"User: {user} does not exists");
                    continue;
                }

                if (!TheUserManager.IsInRole(user, role.Name))
                {
                    var result = await TheUserManager.AddToRoleAsync(user, role.Name);

                    if (!result.Succeeded)
                        ModelState.AddModelError("", $"User: {user} could not be added to role");
                }
            }

            foreach (var user in model.RemovedUsers)
            {
                var appUser = await TheUserManager.FindByIdAsync(user);

                if (appUser == null)
                {
                    ModelState.AddModelError("", $"User: {user} does not exists");
                    continue;
                }

                var result = await TheUserManager.RemoveFromRoleAsync(user, role.Name);

                if (!result.Succeeded)
                    ModelState.AddModelError("", $"User: {user} could not be removed from role");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
    }
}
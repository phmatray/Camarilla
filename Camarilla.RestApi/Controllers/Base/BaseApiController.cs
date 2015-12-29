using System.Net.Http;
using System.Web.Http;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Camarilla.RestApi.Controllers
{
    /// <summary>
    ///     Used by ApiController to provide some managers.
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        private readonly RoleManager _theRoleManager = null;
        private readonly ApplicationUserManager _theUserManager = null;
        private ModelFactory _modelFactory;

        /// <summary>
        ///     The user manager
        /// </summary>
        protected ApplicationUserManager TheUserManager
            => _theUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

        /// <summary>
        ///     The app role manager
        /// </summary>
        protected RoleManager AppRoleManager
            => _theRoleManager ?? Request.GetOwinContext().GetUserManager<RoleManager>();

        /// <summary>
        ///     The model factory
        /// </summary>
        protected ModelFactory TheModelFactory
            => _modelFactory ?? (_modelFactory = new ModelFactory(Request, TheUserManager));

        /// <summary>
        ///     Get error result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded)
                return null;

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}
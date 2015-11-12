using System.Net.Http;
using System.Web.Http;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Camarilla.RestApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private UserManager _appUserManager = null;

        protected UserManager TheUserManager
        {
            get
            {
                return _appUserManager ?? Request.GetOwinContext().GetUserManager<UserManager>();
            }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.TheUserManager);
                }
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
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

            return null;
        }
    }
}
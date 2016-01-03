using System.Net.Http;
using System.Web.Http;
using Camarilla.RestApi.Controllers.ControllerModels;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Infrastructure.Managers;
using Camarilla.RestApi.Infrastructure.Stores.Concretes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Camarilla.RestApi.Controllers
{
    /// <summary>
    ///     Used by ApiController to provide some managers.
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        private CamarillaContext _context;
        private RoleManager _theRoleManager;
        private ApplicationUserManager _theUserManager;
        private PersonaStore _thePersonaStore;
        private ClanStore _theClanStore;
        private PersonaMailStore _thePersonaMailStore;
        private MailStore _theMailStore;
        private RaceStore _theRaceStore;
        private ModelFactory _modelFactory;

        private CamarillaContext Context
            => _context
            ?? (_context = CamarillaContext.Create());

        /// <summary>
        ///     The user manager
        /// </summary>
        protected ApplicationUserManager TheUserManager
            => _theUserManager 
            ?? (_theUserManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>());

        /// <summary>
        ///     The app role manager
        /// </summary>
        protected RoleManager AppRoleManager
            => _theRoleManager 
            ?? (_theRoleManager = Request.GetOwinContext().GetUserManager<RoleManager>());

        /// <summary>
        ///     The persona store
        /// </summary>
        protected PersonaStore ThePersonaStore
            => _thePersonaStore
            ?? (_thePersonaStore = new PersonaStore(Context));

        /// <summary>
        ///     The clan store
        /// </summary>
        protected ClanStore TheClanStore
            => _theClanStore 
            ?? (_theClanStore = new ClanStore(Context));

        /// <summary>
        ///     The personaMail store
        /// </summary>
        protected PersonaMailStore ThePersonaMailStore
            => _thePersonaMailStore
            ?? (_thePersonaMailStore = new PersonaMailStore(Context));

        /// <summary>
        ///     The mail store
        /// </summary>
        protected MailStore TheMailStore
            => _theMailStore
            ?? (_theMailStore = new MailStore(Context));

        /// <summary>
        ///     The race store
        /// </summary>
        protected RaceStore TheRaceStore
            => _theRaceStore
            ?? (_theRaceStore = new RaceStore(Context));

        /// <summary>
        ///     The model factory
        /// </summary>
        protected ModelFactory TheModelFactory
            => _modelFactory
            ?? (_modelFactory = new ModelFactory(Request, TheUserManager));

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
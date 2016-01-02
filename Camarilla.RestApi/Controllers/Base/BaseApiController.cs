﻿using System.Net.Http;
using System.Web.Http;
using Camarilla.RestApi.ControllerModels;
using Camarilla.RestApi.Infrastructure;
using Camarilla.RestApi.Managers;
using Camarilla.RestApi.Stores.Concretes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Camarilla.RestApi.Controllers
{
    /// <summary>
    ///     Used by ApiController to provide some managers.
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        private RoleManager _theRoleManager;
        private ApplicationUserManager _theUserManager;
        private PersonaStore _thePersonaStore;
        private ClanStore _theClanStore;
        private MailboxStore _theMailboxStore;
        private MailStore _theMailStore;
        private RaceStore _theRaceStore;
        private ModelFactory _modelFactory;

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
            ?? (_thePersonaStore = new PersonaStore(CamarillaContext.Create()));

        /// <summary>
        ///     The clan store
        /// </summary>
        protected ClanStore TheClanStore
            => _theClanStore 
            ?? (_theClanStore = new ClanStore(CamarillaContext.Create()));

        /// <summary>
        ///     The mailbox store
        /// </summary>
        protected MailboxStore TheMailboxStore
            => _theMailboxStore
            ?? (_theMailboxStore = new MailboxStore(CamarillaContext.Create()));

        /// <summary>
        ///     The mail store
        /// </summary>
        protected MailStore TheMailStore
            => _theMailStore
            ?? (_theMailStore = new MailStore(CamarillaContext.Create()));

        /// <summary>
        ///     The race store
        /// </summary>
        protected RaceStore TheRaceStore
            => _theRaceStore
            ?? (_theRaceStore = new RaceStore(CamarillaContext.Create()));

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
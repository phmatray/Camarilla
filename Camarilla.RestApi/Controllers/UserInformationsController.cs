﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/userInformations")]
    public class UserInformationsController : ApiController
    {
        /// <summary>
        ///     Get all user informations
        /// </summary>
        /// <remarks>
        ///     Get an array of all userInformations
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [Route("")]
        [ResponseType(typeof (List<UserInformation>))]
        public IHttpActionResult Get()
        {
            var context = new CamarillaContext();
            var store = new UserInformationStore(context);

            var userInformations = store.UserInformations.ToList();

            return Ok(userInformations);
        }

        /// <summary>
        ///     Get a fake user information
        /// </summary>
        /// <remarks>
        ///     Get an object of a fake object userInformation
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Server Error</response>
        [Route("sample")]
        [ResponseType(typeof (UserInformation))]
        public IHttpActionResult GetSample()
        {
            var userInformation = new UserInformation()
            {
                Id = -1,
                FirstName = "Philippe",
                LastName = "Matray",
                Birthday = new DateTime(1988, 8, 1),
                Gender = Gender.Male
            };

            return Ok(userInformation);
        }
    }
}
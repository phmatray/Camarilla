using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Camarilla.RestApi.Db;
using Camarilla.RestApi.Models;
using Camarilla.RestApi.Stores.Concretes;

namespace Camarilla.RestApi.Controllers
{
    [Route("api/userInformations")]
    public class UserInformationsController : ApiController
    {
        // GET: api/userInformations
        [HttpGet]
        public IEnumerable<UserInformation> Get()
        {
            var context = new CamarillaContext();
            var store = new UserInformationStore(context);

            var userInformations = store.UserInformations.ToList();
            return userInformations;
        }
    }
}
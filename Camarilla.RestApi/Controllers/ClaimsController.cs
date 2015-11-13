using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Camarilla.RestApi.Controllers
{
    [RoutePrefix("api/claims")]
    public class ClaimsController : BaseApiController
    {
        [HttpGet]
        [Authorize]
        [Route("")]
        public IHttpActionResult GetClaims()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = identity.Claims
                .Select(c => new
                {
                    subject = c.Subject.Name,
                    type = c.Type,
                    value = c.Value
                });

            return Ok(claims);
        }
    }
}
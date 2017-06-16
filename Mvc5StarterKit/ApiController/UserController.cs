using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Mvc5StarterKit.ApiController
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("GenerateToken")]
        public string GenerateToken()
        {
            var username = User.Identity.GetUserName();
            var tenantName = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirstValue("tenant");
            var user = new Models.UserInfo { UserName = username, TenantUniqueName = tenantName };
            var token = IzendaBoundary.IzendaTokenAuthorization.GetToken(user);

            return token;
        }
    }
}

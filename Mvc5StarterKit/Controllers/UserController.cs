using System.Web.Mvc;
using Rhino.Licensing;
using Microsoft.AspNet.Identity;

namespace Mvc5StarterKit.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult GenerateToken()
        {
            var username = User.Identity.GetUserName();
            var tenantName = ((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirstValue("tenantName");


            var user = new Models.UserInfo { UserName = username, TenantUniqueName = tenantName };
            var token = IzendaBoundary.IzendaTokenAuthorization.GetToken(user);
            return Json(new { token = token }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetCurrentTenant()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var claims = identity.Claims;


            var tenantName = identity.FindFirstValue("tenantName");
            var tenantId = identity.FindFirstValue("tenantId");

            return Json(new { tenantName = tenantName, tenantId = tenantId }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
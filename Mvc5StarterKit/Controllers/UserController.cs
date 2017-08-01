using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using System;
using Mvc5StarterKit.IzendaBoundary;

namespace Mvc5StarterKit.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult GenerateToken()
        {
            var accountName = User.Identity.GetUserName();
            //accountName is in the format: domain\username
            //we will parse it to get the domain portion and it's also tenant name.
            var s = accountName.Split('\\');
            if (s.Count() != 2)
            {
                throw new ArgumentException("Invalid Account Name format");
            }

            var user = new Models.UserInfo { UserName = s[1], TenantUniqueName = s[0] };
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
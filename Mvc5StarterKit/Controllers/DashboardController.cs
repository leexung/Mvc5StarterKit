using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Mvc5StarterKit.Controllers
{
    public class DashboardController : Controller
    {
        // GET: DashboardViewer
        public ActionResult DashboardViewer(string id)
        {
            var queryString = Request.QueryString;
            dynamic filters = new System.Dynamic.ExpandoObject();
            foreach(string key in queryString.AllKeys)
            {
              ((IDictionary<String, Object>)filters).Add(key, queryString[key]);
            }

            ViewBag.Id = id;
            ViewBag.filters = JsonConvert.SerializeObject(filters);
            return View();
        }
    }
}
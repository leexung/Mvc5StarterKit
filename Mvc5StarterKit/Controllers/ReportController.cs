using Mvc5StarterKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Mvc5StarterKit.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        
        // show report Viewer by id
        public ActionResult ReportViewer(string id)
        {
            var queryString = Request.QueryString;
            dynamic filters = new System.Dynamic.ExpandoObject();
            foreach(string key in queryString.AllKeys)
            {
              ((IDictionary<String, Object>)filters).Add(key, queryString[key]);
            }

            ViewBag.overridingFilterQueries = JsonConvert.SerializeObject(filters); ;
            ViewBag.Id = id;
            return View();
        }

        public ActionResult ReportParts()
        {
            return View();
        }

        public ActionResult AdvancedReportParts()
        {
            return View();
        }
    }
}
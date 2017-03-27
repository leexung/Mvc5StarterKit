using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.Models
{
    public class ReportPartFilter
    {
        public string key { get; set; }
        public bool linkReportFilter { get; set; }
        public string value { get; set; }
    }

    public class ReportPartModel
    {
        public string id { get; set; }
        public List<ReportPartFilter> filters { get; set; }
    }
    public class ReportPartViewModel
    {
        public List<ReportPartModel> ReportParts { get; set; }
    }
}
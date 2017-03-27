using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5StarterKit.Models
{
    public class APIModel
    {
        public APIModel()
        {
            AvailableMethods = new List<SelectListItem>();
        }

        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string TenantUniqueName { get; set; }

        public Guid ConnectionId { get; set; }
        public string ConnectionName { get; set; }
        public string TableName { get; set; }
        public string ViewName { get; set; }
        public string SPName { get; set; }
        public string FunctionName { get; set; }

        public int APIMethodId { get; set; }
        public List<SelectListItem> AvailableMethods { get; set; }
        public string Message { get; set; }
    }
}
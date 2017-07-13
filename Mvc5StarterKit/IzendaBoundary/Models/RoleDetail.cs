using Mvc5StarterKit.IzendaBoundary.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models
{
    public class RoleDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // This corresponds to the 'ID' field in the IzendaTenant table
        public Guid? TenantId { get; set; } 
        public bool Active { get; set; }
        public bool NotAllowSharing { get; set; }

        // This corresponds to the 'TenantID' field in the IzendaTenant table
        public string TenantUniqueName { get; set; }
        public Permission Permission { get; set; }
        public List<QuerySourceModel> VisibleQuerySources { get; set; }
        public List<UserDetail> Users { get; set; }

        public RoleDetail()
        {
            Users = new List<UserDetail>();
        }
    }
}
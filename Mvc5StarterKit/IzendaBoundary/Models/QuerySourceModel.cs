using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models
{
    public class QuerySourceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Type { get; set; }
        public Guid? ParentQuerySourceId { get; set; }
        public Guid? CategoryId { get; set; }
        public bool Selected { get; set; }
        public bool Deleted { get; set; }
        public Guid ConnectionId { get; set; }
        public string ConnectionName { get; set; }
        public List<QuerySourceModel> Childs { get; set; }
        public Guid? DataSourceCategoryId { get; set; }
        public string DataSourceCategoryName { get; set; }
        public string Alias { get; set; }
        public List<QuerySourceFieldModel> QuerySourceFields { get; set; }
        public string QuerySourceCategoryName { get; set; }
        public QuerySourceCategoryModel QuerySourceCategory { get; set; }
        public DateTime? Modified { get; set; }
        public string ExtendedProperties { get; set; }
        public int PhysicalChange { get; set; }
        public int Approval { get; set; }
        public bool Existed { get; set; }
        public bool Checked { get; set; }
    }
}
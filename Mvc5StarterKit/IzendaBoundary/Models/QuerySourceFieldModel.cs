using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models
{
    public class QuerySourceFieldModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string DataType { get; set; }
        public string IzendaDataType { get; set; }
        public bool AllowDistinct { get; set; }
        public bool Visible { get; set; }
        public bool Filterable { get; set; }
    }
}
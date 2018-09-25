using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models
{
    public class ModelErrorModel
    {
        public string Key { get; set; }
        public object Detail { get; set; }
        public List<string> Messages { get; set; }
    }
}
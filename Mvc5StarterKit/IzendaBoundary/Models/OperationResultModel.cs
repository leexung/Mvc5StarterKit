using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models
{
    public class OperationResultModel
    {
        public bool Success { get; set; }
        public List<ModelErrorModel> Messages { get; set; }
        public object Data { get; set; }
    }
}
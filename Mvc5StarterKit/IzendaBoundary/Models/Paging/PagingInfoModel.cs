using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models.Paging
{
    public class PagingInfoModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int SkipItems { get; set; }
        public bool IsLastPage { get; set; }
    }
}
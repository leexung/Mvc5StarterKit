using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.IzendaBoundary.Models.Paging
{
    public class PagedResultModel<T> : PagingInfoModel
    {
        public T Result { get; set; }
    }
}
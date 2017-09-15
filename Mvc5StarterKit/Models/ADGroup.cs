using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Mvc5StarterKit.Models
{
    public class ADGroup
    {
        public string Name { get; set; }

        public string SamAccountName { get; set; }

        public string DistinguishedName { get; set; }

        public string Description { get; set; }

        [DisplayName("Is System Admin")]
        public bool IsExistingInIzenda { get; set; }

        /// <summary>
        /// The flag is used on mvc page to identify the group is whether selected or not
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
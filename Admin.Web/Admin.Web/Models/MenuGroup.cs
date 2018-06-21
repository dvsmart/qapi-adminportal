using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Web.Models
{
    public class MenuGroup : BaseModel
    {
        public string Name { get; set; }

        public bool IsVisible { get; set; }
    }
}
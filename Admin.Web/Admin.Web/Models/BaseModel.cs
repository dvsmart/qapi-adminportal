using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Web.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
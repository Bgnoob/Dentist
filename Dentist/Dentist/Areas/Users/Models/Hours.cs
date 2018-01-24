using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dentist.Areas.Users.Models
{
    public class Hours
    {
        public string DentistName { get; set; }
        public string UserID { get; set; }
      
        public DateTime Hour { get; set; }
    }
}
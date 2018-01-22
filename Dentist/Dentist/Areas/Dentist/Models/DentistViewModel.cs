using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dentist.Areas.Dentist.Models
{
    public class DentistViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Town { get; set; }
        public string Spec { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dentist.Areas.Dentist.Models
{
    public class ReserveHourModel
    {
        public string DentistName { get; set; }
        public string UserID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Required(ErrorMessage = "The date is required")]
        public DateTime Hour { get; set; }

       
    }
}
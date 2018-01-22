using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist.Data.Models
{
    public class Hours
    {
         public int Id { get; set; }
         public DateTime Hour { get; set; }
         public string DentistId { get; set; }
      // public ApplicationUser Dentist { get; set; }
         public string PatientId { get; set; }
       // public ApplicationUser Patientt { get; set; }

    }
}

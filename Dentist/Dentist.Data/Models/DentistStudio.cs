using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist.Data.Models
{
    public class DentistStudio
    {
      
        private ICollection<ApplicationUser> applicationUsers;

        public DentistStudio()
        {
            this.applicationUsers = new HashSet<ApplicationUser>();
        }

         public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(1000, MinimumLength = 2)]
        public string Description { get; set; }
        public string Town { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers
        {
            get
            {
                return this.applicationUsers;
            }
            set
            {
                this.applicationUsers = value;
            }
        }



    }
}

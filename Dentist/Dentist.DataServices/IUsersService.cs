using Dentist.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist.DataServices
{
    public interface IUsersService
    {
        IEnumerable<ApplicationUser> GetInformationAboutUser(string username);

        void UpdateUser(string userId, string firstName, string lastName, int years);
    }
}

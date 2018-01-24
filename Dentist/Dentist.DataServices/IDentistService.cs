using Dentist.Data.DataModels;
using Dentist.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dentist.DataServices
{
    public interface IDentistService
    {
        IEnumerable<DentistStudio> getDentistInformation(string name);
        void updateDentist(string username, string name, string description, string town, string spec);
        IEnumerable<DentistStudio> AllDentist();
        bool CheckFreeHour(DateTime hour, string name);
        void SaveHour(DateTime hour, string dentistName, string userID);
        IEnumerable<Hours> GetMyHours(string id);
    }
}

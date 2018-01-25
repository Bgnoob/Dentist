using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentist.Data.Models;
using Dentist.Data;
using System.Linq;
using System.Collections;

namespace Dentist.DataServices
{
 
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;
        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<ApplicationUser> GetInformationAboutUser(string username)
        {
            ApplicationUser user = this.dbContext.Users.First(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentNullException($"No user with username {username} ");
            }
            return this.dbContext.Users.Where(u=> u.Id == user.Id);
        }

        public void UpdateUser(string userName,string firstName,string lastName,int years)
        {
            var user = 
                this.dbContext.
                Users.
                First(u => u.UserName == userName);

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Years = years;
            this.dbContext.SaveChanges();
        }
        public IEnumerable<Hours> GetMyHours(string id)
        {

            return this.dbContext.Hours.Where(b => b.PatientId == id && DateTime.Compare(DateTime.Now, b.Hour) < 0).ToList();

        }

        public IEnumerable<Hours> History(string id)
        {
            return this.dbContext.Hours.Where(b => b.PatientId == id && DateTime.Compare(DateTime.Now,b.Hour)>0).ToList();

        }
    }
}

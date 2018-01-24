using Dentist.Data;
using Dentist.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dentist.DataServices
{
    public class DentistService : IDentistService
    {
        private readonly ApplicationDbContext dbContext;
        public DentistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<DentistStudio> getDentistInformation(string name)
        {
            ApplicationUser user = this.dbContext.Users
               .First(u => u.UserName == name);

            return this.dbContext.DentistStudio.Where(d => d.Id == user.StudioId);
        }
        public IEnumerable<DentistStudio> AllDentist()
        {
            return this.dbContext.DentistStudio.ToList();
        }
        public void updateDentist(string userName, string name, string description, string town, string spec)
        {
            var user =
                this.dbContext.
                Users.
                First(u => u.UserName == userName);
            if (user.StudioId == null)
            {
                user.StudioId = user.Id;

                this.dbContext.DentistStudio.Add(new DentistStudio
                {
                    Name = name,
                    Description = description,
                    Id = user.Id,
                    Town = town,
                    Spec = spec
                });
                this.dbContext.SaveChanges();
            }
            else
            {
                DentistStudio dentistStudio = this.dbContext.DentistStudio.Where(u => u.Id == user.StudioId).First();
                dentistStudio.Description = description;
                dentistStudio.Name = name;
                dentistStudio.Town = town;
                dentistStudio.Spec = spec;
            }
            this.dbContext.SaveChanges();
        }

        public IEnumerable<Hours> GetMyHours(string id)
        {
           
            return  this.dbContext.Hours.Where(b => b.DentistId == id).ToList();

        }

        public bool CheckFreeHour(DateTime hour, string name)
        {
            var dentistId = this.dbContext.DentistStudio.Where(u => u.Name == name).Select(b => b.Id).FirstOrDefault();
            var free = this.dbContext.Hours.Where(h => h.Hour == hour && h.DentistId == dentistId).Any();
            if (!free)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public void SaveHour(DateTime hour, string dentistName, string userID)
        {
            var dentistId = this.dbContext.DentistStudio.Where(u => u.Name == dentistName).Select(b => b.Id).FirstOrDefault();

            this.dbContext.Hours.Add(new Hours
            {
                Hour = hour,
                DentistId = dentistId,
                PatientId = userID
            });
            this.dbContext.SaveChanges();

        }
    }
}
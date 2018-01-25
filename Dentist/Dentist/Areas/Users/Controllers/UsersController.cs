using Dentist.Data;
using Dentist.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Dentist.Areas.Users.Models;

namespace Dentist.Areas.Users.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {

        private readonly IUsersService usersService;
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;
        public UsersController(ApplicationUserManager userManager, ApplicationDbContext dbContext, IUsersService usersService)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.usersService = usersService;

        }
        public ActionResult Index()
        {
            var viewModel = this.usersService
                .GetInformationAboutUser(this.User.Identity.Name)
                .Select(u => new EditUserViewModel
                {
                    Username=u.UserName,
                    Firstname = u.FirstName,
                    Lastname = u.LastName,
                    Years = u.Years
                });

            return this.View(viewModel);
        }

        public ActionResult MyHours()
        {
            var username = this.User.Identity.Name;
            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var viewmodel = this.usersService.GetMyHours(user.Id).Select(b => new Hours
            {
                Hour = b.Hour,
                DentistName = b.DentistId,
                UserID = b.PatientId
            });

            return this.View(viewmodel);
        }
        public ActionResult History()
        {
            var username = this.User.Identity.Name;
            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var viewmodel = this.usersService.History(user.Id).Select(b => new Hours
            {
                Hour = b.Hour,
                DentistName = b.DentistId,
                UserID = b.PatientId
            });

            return this.View(viewmodel);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(EditUserViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.usersService.UpdateUser(this.User.Identity.Name, viewModel.Firstname, viewModel.Lastname, viewModel.Years);
                    return this.RedirectToAction("Index");
            }
            return this.View(viewModel);
        }
    }
}
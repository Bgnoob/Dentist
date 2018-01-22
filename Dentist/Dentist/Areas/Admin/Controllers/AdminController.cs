using Dentist.Areas.Admin.Models;
using Dentist.Data;
using Dentist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dentist.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;
        public AdminController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;

        }
        public ActionResult AllUsers()
        {
            var usersViewModel = this.userManager
                .Users
                .Select(u => new UserViewModel()
                {
                    Username = u.UserName
                })
                .ToList();
            return this.View(usersViewModel);
        }
        public async Task<ActionResult> EditUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userViewModel = UserViewModel.Create.Compile()(user);

            userViewModel.IsAdmin = await this.userManager.IsInRoleAsync(user.Id, "Admin");
            userViewModel.IsDentist = await this.userManager.IsInRoleAsync(user.Id, "Dentist");
            return this.PartialView("_EditUser", userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(UserViewModel userViewModel)
        {
            if (userViewModel.IsAdmin)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Admin");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Admin");
            }

            if (userViewModel.IsDentist)
            {
                await this.userManager.AddToRoleAsync(userViewModel.Id, "Dentist");
            }
            else
            {
                await this.userManager.RemoveFromRoleAsync(userViewModel.Id, "Dentist");
            }
            return this.RedirectToAction("AllUsers");
        }
    }
}
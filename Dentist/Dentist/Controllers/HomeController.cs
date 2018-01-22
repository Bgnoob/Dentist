using Dentist.Data;
using Dentist.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dentist.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;
        public HomeController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }
        public ActionResult Index()
        {
            return View();
        }
      //  [Authorize(Roles ="Admin")]
        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            
            //var user = await this.userManager.FindByNameAsync(this.User.Identity.Name);
            //await this.userManager.AddToRoleAsync(user.Id, "Admin");
 
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //this.dbContext.Roles.Add(new IdentityRole() { Name = "Dentist" });
            //this.dbContext.SaveChanges();
            return View();
        }
    }
}
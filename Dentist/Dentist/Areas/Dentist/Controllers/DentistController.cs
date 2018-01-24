using Dentist.Data;
using Dentist.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Dentist.Areas.Dentist.Models;

namespace Dentist.Areas.Dentist.Controllers
{
    public class DentistController : Controller
    {
        private readonly IDentistService dentistService;
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationDbContext dbContext;
        public DentistController(ApplicationUserManager userManager, ApplicationDbContext dbContext, IDentistService dentistService)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.dentistService = dentistService;

        }
        public ActionResult AllDentist()
        {
            var dentistViewModel = this.dentistService.AllDentist().Select
                (u => new DentistViewModel
                {
                    Name = u.Name,
                    Description = u.Description,
                    Town = u.Town,
                    Spec = u.Spec
                });
            return this.View(dentistViewModel);
        }

        public ActionResult Index()
        {
            var username = this.User.Identity.Name;

            var viewModel = this.dentistService
                .getDentistInformation(username)
                .Select(b => new DentistViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    Town = b.Town,
                    Spec = b.Spec

                });



            return this.View(viewModel);
        }
        public ActionResult ReserveHour()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReserveHour(ReserveHourModel reserveHourModel)
        {
            var username = this.User.Identity.Name;

            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            if (this.dentistService.CheckFreeHour(reserveHourModel.Hour, reserveHourModel.DentistName))
            {
                this.dentistService.SaveHour(reserveHourModel.Hour, reserveHourModel.DentistName, user.Id);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DentistViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.dentistService.updateDentist(this.User.Identity.Name, viewModel.Name, viewModel.Description, viewModel.Town, viewModel.Spec);
                return this.RedirectToAction("Index");
            }
            return this.View(viewModel);
        }

        public ActionResult DentistHours()
        {
            var username = this.User.Identity.Name;
            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var viewmodel = this.dentistService.GetMyHours(user.Id).Select(b => new ReserveHourModel
            {
                Hour = b.Hour,
                DentistName = b.DentistId,
                UserID = b.PatientId
            });

            return this.View(viewmodel);
        }


        [HttpGet]
        public ActionResult ShowHours(int? month, int? date)
        {
            if (month == null || month < 1 || month > 12)
                ViewBag.month = (DateTime.Now.Month);
            else
                ViewBag.month = month;
            if (date == null || date < 1 || (date > 29 && month == 2) || date > 31)
                ViewBag.date = (int)DateTime.Now.Day;
            else
                ViewBag.date = date;
            return this.View();
        }
        public ActionResult Calendar(int? month)
        {
            if (month == null || month < 1 || month > 12)
                ViewBag.month = (DateTime.Now.Month);
            else
                ViewBag.month = month;
            return this.View();
        }
    }
}
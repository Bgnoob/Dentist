using Dentist.Data;
using Dentist.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Dentist.Areas.Dentist.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

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
        public async Task<ActionResult> ReserveHour(ReserveHourModel reserveHourModel)
        {
            var username = this.User.Identity.Name;

            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            if (this.dentistService.CheckFreeHour(reserveHourModel.Hour, reserveHourModel.DentistName))
            {
                this.dentistService.SaveHour(reserveHourModel.Hour, reserveHourModel.DentistName, user.Id);
            }
            if (ModelState.IsValid)
            {

                var text = $"You have booked an appointment at: {reserveHourModel.Hour}. Your dentist will be {reserveHourModel.DentistName}.";
                var message = new MailMessage();
                message.To.Add(new MailAddress(user.Email));
                message.From = new MailAddress("dentistsite123@gmail.com");
                message.Subject = "Dentist";
                message.Body = text;


                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "dentistsite123@gmail.com",
                        Password = "dentist123"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                  //  return RedirectToAction("ReserveHour");
                }
            }

            return RedirectToAction("AllDentist");
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
            var username = this.User.Identity.Name;
            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var viewmodel = this.dentistService.GetMyHours(user.Id).Select(b => new ReserveHourModel
            {
                Hour = b.Hour,
                DentistName = b.DentistId,
                UserID = b.PatientId
            });
            var userid = User.Identity.GetUserId();
            if (month == null || month < 1 || month > 12)
                ViewBag.month = (DateTime.Now.Month);
            else
                ViewBag.month = month;
            ViewBag.userid = userid;
            ViewBag.isOwner = (user.Id == userid) ? true : false;
            if (date == null || date < 1 || (date > 29 && month == 2) || date > 31)
                ViewBag.date = (int)DateTime.Now.Day;
            else
                ViewBag.date = date;
            return this.View(viewmodel);
        }
        public ActionResult Calendar(int? month)
        {
            var username = this.User.Identity.Name;
            var user = this.dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var viewmodel = this.dentistService.GetMyHours(user.Id).Select(b => new ReserveHourModel
            {
                Hour = b.Hour,
                DentistName = b.DentistId,
                UserID = b.PatientId
            });
            var userid = User.Identity.GetUserId();
            if (month == null || month < 1 || month > 12)
                ViewBag.month = (DateTime.Now.Month);
            else
                ViewBag.month = month;
            ViewBag.userid = userid;
            ViewBag.isOwner = (user.Id == userid) ? true : false;
            return this.View(viewmodel);
        }
    }
}
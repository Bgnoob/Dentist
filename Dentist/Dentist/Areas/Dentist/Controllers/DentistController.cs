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
    public class DentistController: Controller
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
                {Name=u.Name,
                Description=u.Description,
                Town=u.Town,
                Spec=u.Spec
                });
            return this.View(dentistViewModel);
        }

        public ActionResult Index()
        {
            var username = this.User.Identity.Name;
           
            var viewModel = this.dentistService
                .getDentistInformation(username)
                .Select(b=> new DentistViewModel {
                  Id=b.Id,
                  Name=b.Name,
                  Description=b.Description,
                  Town=b.Town,
                  Spec=b.Spec
                  
                });



            return this.View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DentistViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.dentistService.updateDentist(this.User.Identity.Name, viewModel.Name, viewModel.Description,viewModel.Town,viewModel.Spec);
                return this.RedirectToAction("Index");
            }
            return this.View(viewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTermin.Controllers {
    public class AdministratorController : Controller {
        private DatabaseContext database = DatabaseContext.getInstance();

        public IActionResult Index() {
            return View();
        }

        public IActionResult AdministratorSignOut_OnClick() {
            database.Log.Add(new Log {
                DateTime = DateTime.Now,
                Note = "Administrator \"" + LoginController.currentyLoggedAdministrator.Username + "\" has signed out.",
                PersonID = LoginController.currentyLoggedAdministrator.AdministratorID
            });
            LoginController.currentyLoggedAdministrator = null;
            database.SaveChanges();
            return View("../Login/Index");

        }
    }
}

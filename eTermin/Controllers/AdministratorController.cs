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
            LoginController.currentyLoggedAdministrator = null;
            return View("../Login/Index");

        }
    }
}

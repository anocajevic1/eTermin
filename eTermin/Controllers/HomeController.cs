using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eTermin.Models;
using Microsoft.EntityFrameworkCore;

namespace eTermin.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            var baza = DatabaseContext.getInstance();
            var i = baza.Administrator;
            i.ToList().ForEach((Administrator admin) => {
                string test = admin.Username;
                Console.WriteLine(test);
            });
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RecoveryForm1() {
            return View();
        }
    }
}

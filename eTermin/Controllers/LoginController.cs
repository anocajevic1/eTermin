using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eTermin.Models;
using Microsoft.EntityFrameworkCore;

namespace eTermin.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            /*
            var baza = DatabaseContext.getInstance();
            var i = baza.Administrator;
            i.ToList().ForEach((Administrator admin) => {
                string test = admin.Username;
                Console.WriteLine(test);
            });
            */
            /*
            User user = new User {
                FirstName = "Faris",
                LastName = "Sisic",
                Balance = 1000,
                Email = "fsisic@fmail.com",
                Username = "fsisic",
                Password = "test",
                Photo = null,
            };
            Employee employee = new Employee {
                FirstName = "Faris",
                LastName = "Poljcic",
                Email = "fpoljcic@fmail.com",
                Username = "fpoljcic",
                Password = "test",
                SportCentre = null,
            };
            var baza = DatabaseContext.getInstance();
            baza.Person.Add(user);
            baza.SaveChanges();
            baza.Person.Add(employee);
            baza.SaveChanges();
            */
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

        public IActionResult RegistrationForm() {
            return View();
        }
    }
}

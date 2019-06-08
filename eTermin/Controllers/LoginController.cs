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
        private static DatabaseContext database = DatabaseContext.getInstance();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult StartPasswordRecovery() {
            return View("RecoveryForm1");
        }

        [HttpPost]
        public IActionResult SignIn(string etUsername, string etPasswod) {
            var dataAdmin = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername) && administrator.Password.Equals(etPasswod));
            var data = database.Person.Where((Person person) => person.Username.Equals(etUsername) && person.Password.Equals(etPasswod));
            if (data.Count() == 0 && dataAdmin.Count() == 0)
                return View("Index");
            else if (dataAdmin.Count() == 0) {
                // User || Employee
                Person person = data.First();
                if (person is User)
                    return View("../User/Index", person);
                else if (person is Employee)
                    return View("../Employee/Index", person);
            }
            // Administrator
            return View("../Administrator/Index", dataAdmin.First());
        }

    }
}

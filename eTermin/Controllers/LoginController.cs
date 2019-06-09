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
        public static Person currentyLoggedPerson = null;
        public static Administrator currentyLoggedAdministrator = null;

        public IActionResult Index() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SignIn(string etUsername, string etPassword) {
            var dataAdmin = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername) && administrator.Password.Equals(etPassword));
            var data = database.Person.Where((Person person) => person.Username.Equals(etUsername) && person.Password.Equals(etPassword));
            if (data.Count() == 0 && dataAdmin.Count() == 0)
                return View("Index");
            else if (dataAdmin.Count() == 0) {
                // User || Employee
                currentyLoggedPerson = data.First();
                if (currentyLoggedPerson is User) {
                    database.Log.Add(new Log {
                        DateTime = DateTime.Now,
                        Note = "User \"" + currentyLoggedPerson.Username + "\" has signed in.",
                        PersonID = currentyLoggedPerson.PersonID
                    });
                    database.SaveChanges();
                    return View("../User/Index", currentyLoggedPerson);
                } else if (currentyLoggedPerson is Employee) {
                    database.Log.Add(new Log {
                        DateTime = DateTime.Now,
                        Note = "Employee \"" + currentyLoggedPerson.Username + "\" has signed in.",
                        PersonID = currentyLoggedPerson.PersonID
                    });
                    database.SaveChanges();
                    return View("../Employee/Index", currentyLoggedPerson);
                }
            }
            currentyLoggedAdministrator = dataAdmin.First();
            return View("../Administrator/Index", currentyLoggedAdministrator);
        }

    }
}

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
        public IActionResult SignIn(string etUsername, string etPassword) {
            var dataAdmin = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername) && administrator.Password.Equals(etPassword));
            var data = database.Person.Where((Person person) => person.Username.Equals(etUsername) && person.Password.Equals(etPassword));
            if (data.Count() == 0 && dataAdmin.Count() == 0)
                return View("Index");
            else if (dataAdmin.Count() == 0) {
                // User || Employee
                currentyLoggedPerson = data.First();
                if (currentyLoggedPerson is User)
                {
                    database.Log.Add(new Log
                    {
                        DateTime = DateTime.Now,
                        Note = "User \"" + currentyLoggedPerson.Username + "\" has signed in.",
                        PersonID = currentyLoggedPerson.PersonID
                    });
                    database.SaveChanges();
                    return View("../User/Index",currentyLoggedPerson);
                }
                else if (currentyLoggedPerson is Employee) {
                    database.Log.Add(new Log
                    {
                        DateTime = DateTime.Now,
                        Note = "Employee \"" + currentyLoggedPerson.Username + "\" has signed in.",
                        PersonID = currentyLoggedPerson.PersonID
                    });
                    database.SaveChanges();
                    return View("../Employee/Index",currentyLoggedPerson);
                }
            }
            currentyLoggedAdministrator = dataAdmin.First();
            database.Log.Add(new Log
            {
                DateTime = DateTime.Now,
                Note = "Administrator \"" + currentyLoggedAdministrator.Username + "\" has signed in.",
                PersonID = currentyLoggedAdministrator.AdministratorID
            });
            database.SaveChanges();
            return View("../Administrator/Index",currentyLoggedAdministrator);
        }

    }
}

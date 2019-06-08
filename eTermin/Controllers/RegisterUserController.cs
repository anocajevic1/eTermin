using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;

namespace eTermin.Controllers
{
    public class RegisterUserController : Controller
    {

        private static DatabaseContext database = DatabaseContext.getInstance();
        public IActionResult Index()
        {
           
            return View("RegistrationForm");
        }
        [HttpPost]
        public IActionResult Validate(string etFirstName,string etLastName,string etEmail,string etUsername,string etPassword,string etConfirmPassword)
        {
            Boolean validationOk = true;
            if (etFirstName.Equals("") || etLastName.Equals("") || etUsername.Equals("") || etPassword.Equals("") || etConfirmPassword.Equals("") || etEmail.Equals("")) validationOk = false;
            if (!etPassword.Equals(etConfirmPassword)) validationOk = false;

            var people = database.Person.Where((Person person) => person.Username.Equals(etUsername));
            var admins = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername));
            if (people.Count() != 0 || admins.Count() != 0) validationOk = false;

            if ( validationOk)
            {
                database.Person.Add(new User
                {
                    FirstName = etFirstName,
                    LastName = etLastName,
                    Username = etUsername,
                    Password = etPassword,
                    Email = etEmail,
                    Balance = 0,
                    Photo = null
                });
                database.SaveChanges();
                var peopleUpdated = database.Person.Where((Person person) => person.Username.Equals(etUsername)).ToList();
                Person thisPerson = peopleUpdated[0];

                database.Log.Add(new Log
                {
                    DateTime = DateTime.Now,
                    Note = "New user \"" + thisPerson.Username + "\" registered",
                    PersonID = thisPerson.PersonID

                });
                database.SaveChanges();
                return View("../Login/Index");
            }
            return View("RegistrationForm");
        }
    }
}
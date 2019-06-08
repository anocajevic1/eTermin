using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTermin.Controllers
{
    public class UserController : Controller
    {

        private DatabaseContext database = DatabaseContext.getInstance();
        
        public IActionResult Index()
        {

            return View("Index",LoginController.currentyLoggedPerson);
        
        }
        public IActionResult TabSportsCentres()
        {
            return View("UserSportsCentres");

        }
        public IActionResult TabCustomReservation()
        {
            return View("UserCustomReservation");

        }
        public IActionResult TabMyReservations()
        {
            return View("UserMyReservations",LoginController.currentyLoggedPerson);

        }
        public IActionResult TabMyAccount()
        {
            return View("UserMyAccount",LoginController.currentyLoggedPerson);

        }
        public IActionResult TabAbout()
        {
            return View("UserAbout");

        }
        public IActionResult UserSignOut_OnClick()
        {
            database.Log.Add(new Log
            {
                DateTime = DateTime.Now,
                Note = "User \"" + LoginController.currentyLoggedPerson.Username + "\" has signed out.",
                PersonID = LoginController.currentyLoggedPerson.PersonID
            });
            LoginController.currentyLoggedPerson = null;
            database.SaveChanges();
            return View("../Login/Index");

        }
        public IActionResult MyAccountSaveEditing_onClick(string etFirstName,string etLastName,string etUsername, string etEmail, string etPassword,string etConfirmPassword)
        {
            Boolean validationOk = true;
            if (etFirstName.Equals("") || etLastName.Equals("") || etUsername.Equals("") || etPassword.Equals("") || etConfirmPassword.Equals("") || etEmail.Equals("")) validationOk = false;
            if (!etPassword.Equals(etConfirmPassword)) validationOk = false;

            var people = database.Person.Where((Person person) => person.Username.Equals(etUsername));
            var admins = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername));
            if ((people.Count() != 0 || admins.Count() != 0) || ( people.Count() == 1 && LoginController.currentyLoggedPerson.Username.Equals(etUsername ))) validationOk = false;
            if (validationOk)
            {
                LoginController.currentyLoggedPerson.FirstName = etFirstName;
                LoginController.currentyLoggedPerson.LastName = etLastName;
                LoginController.currentyLoggedPerson.Email = etEmail;
                LoginController.currentyLoggedPerson.Username = etUsername;
                LoginController.currentyLoggedPerson.Password = etPassword;
                database.Person.Update(LoginController.currentyLoggedPerson);
                database.SaveChanges();
   
            }
            return View("UserMyAccount", LoginController.currentyLoggedPerson);
        }

    }
}

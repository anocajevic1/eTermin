using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eTermin.Controllers {
    public class GiftController : Controller {
        private static DatabaseContext database = DatabaseContext.getInstance();

        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }

        public IActionResult GiftCurrency(string etUsername, double etCurrency) {
            if (etUsername == null || etCurrency < 0 || etCurrency > ((User)LoginController.currentyLoggedPerson).Balance) return View("Index");
            if (etUsername.Equals(LoginController.currentyLoggedPerson.Username)) return View("Index");
            var data = database.Person.Where((Person person) => person.Username.Equals(etUsername));
            if (data.Count() == 0) return View("Index");
            User giftReciever = (User)data.First();
            giftReciever.Balance += etCurrency;
            database.Person.Update(giftReciever);
            ((User)LoginController.currentyLoggedPerson).Balance -= etCurrency;
            database.Person.Update(LoginController.currentyLoggedPerson);
            database.SaveChanges();
            return View("../User/UserMyAccount", LoginController.currentyLoggedPerson);
        }

        public IActionResult Cancel_onClick() {
            return View("../User/UserMyAccount", LoginController.currentyLoggedPerson);
        }
    }
}

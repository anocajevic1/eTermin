using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers {
    public class AdminController : Controller {
        private DatabaseContext database = DatabaseContext.getInstance();
        public static string filter;
        public static string filterSearchCentres;
        public static string selectedFilter;
        public static string searchInput;

        public IActionResult Index() {
            return View("Index", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult AdminSignOut_OnClick() {
            LoginController.currentyLoggedAdministrator = null;
            return View("../Login/Index");
        }

        public IActionResult TabSportsCentres() {
            return View("AdminSportsCentresForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult TabUsers() {
            return View("AdminUsersForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult TabTransactionsHistory() {
            return View("AdminTransactionsForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult DeleteSportCentre(string etName, string etAddress) {
            SportCentre sportCentre = database.FindSportCentre(etName, etAddress);
            database.SportCentre.Remove(sportCentre);
            database.SaveChanges();
            return View("AdminSportsCentresForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult EditSportCentre(string etName, string etAddress) {
            return View("AdminAddEditForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult AddSportCentre() {
            return View("AdminAddEditForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult Search(string etSearch) {
            filter = etSearch;
            return View("Index", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult SearchSportCentres(string etSearch) {
            filterSearchCentres = etSearch;
            return View("AdminSportsCentresForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult SearchUsers(string etSearchFilter, string searchField) {
            selectedFilter = etSearchFilter;
            searchInput = searchField;

            return View("AdminUsersForm", LoginController.currentyLoggedAdministrator);
        }

        public IActionResult DeleteUser(int personID)
        {
            Person p = database.Person.Where((Person person) => person.PersonID == personID).First();
            var logs = database.Log.Where((Log log) => log.PersonID == personID).ToList();
            for (int i = 0; i < logs.Count; i++)
            {
                database.Log.Remove(logs[i]);
                database.SaveChanges();
            }
            database.Person.Remove(p);
            database.SaveChanges();
            return View("AdminUsersForm", LoginController.currentyLoggedAdministrator);
        }
    }
}

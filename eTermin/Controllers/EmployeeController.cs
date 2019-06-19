using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;

namespace eTermin.Controllers {
    public class EmployeeController : Controller {
        private DatabaseContext database = DatabaseContext.getInstance();
        public static DateTime selectedDate;
        public static int selectedSportCentre = ((Employee)LoginController.currentyLoggedPerson).SportCentreID;
        public static string selectedSport;
        public static string filter;

        public IActionResult Index() {
            return View("Index", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabReservations() {
            return View("EmployeeReservationsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabTransactions() {
            return View("EmployeeTransactionsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult EmployeeSignOut_OnClick() {
            database.Log.Add(new Log {
                DateTime = DateTime.Now,
                Note = "Employee \"" + LoginController.currentyLoggedPerson.Username + "\" has signed out.",
                PersonID = LoginController.currentyLoggedPerson.PersonID
            });
            LoginController.currentyLoggedPerson = null;
            database.SaveChanges();
            return View("../Login/Index");
        }

        public IActionResult Search(string etSearch) {
            filter = etSearch;
            return View("Index", LoginController.currentyLoggedPerson);
        }
        public IActionResult Gift() {
            return View("EmployeeNewTransaction", LoginController.currentyLoggedPerson);
        }

        public IActionResult Filter(DateTime etDate, string etSport) {
            selectedDate = etDate;
            selectedSport = etSport;
            return View("EmployeeReservationsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult CreateReservation(string etTime, int etPrice) {
            Employee employee = (Employee)LoginController.currentyLoggedPerson;
            int hour = int.Parse(etTime.Substring(0, 2));
            int min = int.Parse(etTime.Substring(3, 2));
            int id = database.GetHallID(selectedSportCentre, selectedSport);
            selectedDate = selectedDate.AddHours(hour).AddMinutes(min);
            database.Reservation.Add(new Reservation {
                DateTimeCreated = DateTime.Now,
                DateTime = selectedDate,
                HallID = database.GetHallID(selectedSportCentre, selectedSport),
                PersonID = LoginController.currentyLoggedPerson.PersonID
            });
            database.SaveChanges();
            // selectedDate = null; DateTime ne moze bit null
            selectedSport = null;

            return View("EmployeeReservationsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult RemoveReservation(string etTime, int etPrice) {
            Employee employee = (Employee)LoginController.currentyLoggedPerson;
            int hour = int.Parse(etTime.Substring(0, 2));
            int min = int.Parse(etTime.Substring(3, 2));
            int id = database.GetHallID(selectedSportCentre, selectedSport);
            selectedDate = selectedDate.AddHours(hour).AddMinutes(min);
            Reservation reservation = database.FindReservation(etTime, selectedDate, selectedSportCentre, selectedSport);
            database.Reservation.Remove(reservation);

            database.SaveChanges();
            // selectedDate = null; DateTime ne moze bit null
            selectedSport = null;

            return View("EmployeeReservationsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult MakeTransaction(string etUsername, double etCurrency) {
            var data = database.Person.Where((Person person) => person.Username.Equals(etUsername));
            if (data.Count() == 0) return View("EmployeeTransactionsForm", LoginController.currentyLoggedPerson);
            User giftReciever = (User)data.First();
            giftReciever.Balance += etCurrency;
            database.Person.Update(giftReciever);
            database.SaveChanges();
            database.Transaction.Add(new Transaction() {
                Time = DateTime.Now,
                Amount = (int)etCurrency,
                SportCentreID = selectedSportCentre,
                UserID = giftReciever.PersonID,
                EmployeeID = LoginController.currentyLoggedPerson.PersonID
            });
            database.SaveChanges();
            return View("EmployeeTransactionsForm", LoginController.currentyLoggedPerson);
        }

        public IActionResult Cancel_onClick() {
            return View("EmployeeTransactionsForm", LoginController.currentyLoggedPerson);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Models;
using Microsoft.AspNetCore.Mvc;


namespace eTermin.Controllers {
    public class UserController : Controller {
        private DatabaseContext database = DatabaseContext.getInstance();
        public static DateTime selectedDate;
        public static string selectedSportCentre;
        public static string selectedSport;
        public static string selectedSportCentre_MyReservations;
        public static string selectedSport_myReservations;

        public IActionResult Index() {
            return View("Index", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabSportsCentres() {
            return View("UserSportsCentres", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabCustomReservation() {
            return View("UserCustomReservation");
        }

        public IActionResult TabMyReservations() {
            return View("UserMyReservations", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabMyAccount() {
            return View("UserMyAccount", LoginController.currentyLoggedPerson);
        }

        public IActionResult TabAbout() {
            return View("UserAbout", LoginController.currentyLoggedPerson);
        }

        public IActionResult UserSignOut_OnClick() {
            database.Log.Add(new Log {
                DateTime = DateTime.Now,
                Note = "User \"" + LoginController.currentyLoggedPerson.Username + "\" has signed out.",
                PersonID = LoginController.currentyLoggedPerson.PersonID
            });
            LoginController.currentyLoggedPerson = null;
            database.SaveChanges();
            return View("../Login/Index");
        }

        public IActionResult MyAccountSaveEditing_onClick(string etFirstName, string etLastName, string etUsername, string etEmail, string etPassword, string etConfirmPassword) {
            bool validationOk = true;
            if (etFirstName.Equals("") || etLastName.Equals("") || etUsername.Equals("") || etPassword.Equals("") || etConfirmPassword.Equals("") || etEmail.Equals("")) validationOk = false;
            if (!etPassword.Equals(etConfirmPassword)) validationOk = false;

            var people = database.Person.Where((Person person) => person.Username.Equals(etUsername));
            var admins = database.Administrator.Where((Administrator administrator) => administrator.Username.Equals(etUsername));
            if ((admins.Count() != 0) || (people.Count() != 0 && !LoginController.currentyLoggedPerson.Username.Equals(etUsername))) validationOk = false;
            if (validationOk) {
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

        public IActionResult SearchMyReservations(string etSportCentre, string etSport) {
            selectedSportCentre_MyReservations = etSportCentre;
            selectedSport_myReservations = etSport;
            return View("UserMyReservations", LoginController.currentyLoggedPerson);
        }

        public IActionResult Search(DateTime etDate, string etSportCentre, string etSport) {
            selectedDate = etDate;
            selectedSportCentre = etSportCentre;
            selectedSport = etSport;
            return View("UserSportsCentres", LoginController.currentyLoggedPerson);
        }

        public IActionResult CreateReservation(string etTime, int etPrice) {
            User user = (User)LoginController.currentyLoggedPerson;
            if (etPrice > user.Balance)
                return View("UserSportsCentres", LoginController.currentyLoggedPerson);
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
            user.Balance -= etPrice;
            database.Person.Update(user);
            database.SaveChanges();

            return View("UserMyReservations", LoginController.currentyLoggedPerson);
        }

        public IActionResult DeleteReservation(int reservationID)
        {
            Reservation r = database.Reservation.Where((Reservation reservation) => reservation.ReservationID == reservationID).First();
            Hall h = database.Hall.Where((Hall hall) => hall.HallID == r.HallID).First();
            double PRICE = h.Price;
            database.Reservation.Remove(r);
            DateTime timeReserved = r.DateTime;
            DateTime currentTime = DateTime.Now;
            var hours = (timeReserved - currentTime).TotalHours;
            if (hours > 48) ((User)LoginController.currentyLoggedPerson).Balance += PRICE;
            if (hours < 48 && hours > 24) ((User)LoginController.currentyLoggedPerson).Balance += (PRICE / 2);
            database.Person.Update(LoginController.currentyLoggedPerson);
            database.SaveChanges();
            return View("UserMyReservations", LoginController.currentyLoggedPerson);
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTermin.Controllers;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace eTermin.Models {
    public class DatabaseContext : DbContext {
        public static DatabaseContext instance;

        public static DatabaseContext getInstance() {
            return instance;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
            instance = this;
        }

        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<SportCentre> SportCentre { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Administrator>().ToTable("Administrator");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Hall>().ToTable("Hall");
            modelBuilder.Entity<Log>().ToTable("Log");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<SportCentre>().ToTable("SportCentre");
            modelBuilder.Entity<Subscription>().ToTable("Subscription");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<User>().ToTable("User");
        }

        public List<Reservation> Reservations(Person person) {
            List<Reservation> reservations = Reservation.Where((Reservation reservation) => reservation.PersonID.Equals(person.PersonID)).ToList();
            reservations.Sort((Reservation a, Reservation b) => DateTime.Compare(a.DateTime, b.DateTime));
            return reservations;
        }

        public List<Reservation> Reservations(DateTime dateTime) {
            List<Reservation> reservations = Reservation.Where((Reservation reservation) => reservation.DateTime.Date.Equals(dateTime.Date)).ToList();
            reservations.Sort((Reservation a, Reservation b) => DateTime.Compare(b.DateTime, a.DateTime));
            return reservations;
        }

        public List<Transaction> Transactions(Person person, DateTime dateTime) {
            List<Transaction> transactions = Transaction.Where((Transaction transaction) => transaction.EmployeeID.Equals(person.PersonID) && transaction.Time.Date.Equals(dateTime.Date)).ToList();
            transactions.Sort((Transaction a, Transaction b) => DateTime.Compare(b.Time, a.Time));
            return transactions;
        }

        public List<SportCentre> SportCentres(string filter) {
            List<SportCentre> sportCentres = SportCentre.ToList();
            if (filter != null) {
                sportCentres.RemoveAll((SportCentre sc) => {
                    return !sc.Name.ToLower().Contains(sc.Name.ToLower());
                });
            }
            return sportCentres;
        }

        public Employee Employee(int sportCentreID) {
            return (Employee)Person.Where((Person person) => person is Employee && ((Employee)person).SportCentreID == sportCentreID).First();
        }

        public List<Transaction> Transactions(Person person) {
            List<Transaction> transactions = Transaction.Where((Transaction transaction) => transaction.EmployeeID.Equals(person.PersonID)).ToList();
            transactions.Sort((Transaction a, Transaction b) => DateTime.Compare(b.Time, a.Time));
            return transactions;
        }

        public SportCentre GetSportCentre(int hallID) {
            return SportCentre.Where((SportCentre sportCentre) => GetHall(hallID).SportCentreID.Equals(sportCentre.SportCentreID)).First();
        }

        public SportCentre GetSportCentreByID(int sportCentreID) {
            return SportCentre.Where((SportCentre sportCentre) => sportCentreID == sportCentre.SportCentreID).First();
        }

        public Hall GetHall(int hallID) {
            return Hall.Find(hallID);
        }

        public string GetUsername(string email) {
            var persons = Person.Where((Person person) => person.Email.Equals(email));
            if (persons.Count() == 0)
                return null;
            else
                return persons.First().Username;
        }

        public void UpdatePassword(string username, string etPassword) {
            var user = Person.Where((Person person) => person.Username.Equals(username)).First();
            user.Password = etPassword;
            SaveChanges();
        }

        public Reservation FindReservation(string etTime, DateTime selectedDate, int selectedSportCentre, string selectedSport) {
            int hallID = GetHallID(selectedSportCentre, selectedSport);
            return Reservation.Where((Reservation res) => res.DateTime.Date.Equals(selectedDate.Date) && etTime.Equals(res.DateTime.ToShortTimeString()) && res.HallID.Equals(hallID)).First();
        }

        public SportCentre FindSportCentre(string etName, string etAddress) {
            return SportCentre.Where((SportCentre sc) => sc.Name.Equals(etName) && sc.Address.Equals(etAddress)).First();
        }

        public List<Reservation> MyReservations() {
            string sportCentre = UserController.selectedSportCentre_MyReservations;
            string sport = UserController.selectedSport_myReservations;
            if (sportCentre != null && sportCentre.Equals("All sport centres")) sportCentre = null;
            if (sport != null && sport.Equals("All sports")) sport = null;
            List<Reservation> reservations = Reservations(LoginController.currentyLoggedPerson);
            if (sportCentre == null && sport == null)
                return reservations;
            if (sportCentre == null && sport != null) {
                for (int i = 0; i < reservations.Count; i++) {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if (!h.Sport.ToString().Equals(sport)) {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            }
            if (sportCentre != null && sport == null) {
                SportCentre SC = SportCentre.Where((SportCentre SportCentre) => SportCentre.Name.Equals(sportCentre)).First();
                for (int i = 0; i < reservations.Count; i++) {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if (!h.SportCentreID.Equals(SC.SportCentreID)) {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            } else if (sportCentre != null && sport != null) {
                SportCentre sc = SportCentre.Where((SportCentre SportCentre) => SportCentre.Name.Equals(sportCentre)).First();
                for (int i = 0; i < reservations.Count; i++) {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if (!h.SportCentreID.Equals(sc.SportCentreID) || !h.Sport.ToString().Equals(sport)) {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            }
            UserController.selectedSportCentre_MyReservations = null;
            UserController.selectedSport_myReservations = null;
            return reservations;
        }

        public List<Transaction> MyTransactions(string filter) {
            List<Transaction> transactions = Transactions(LoginController.currentyLoggedPerson, DateTime.Now);
            if (filter != null) {
                transactions.RemoveAll((Transaction tr) => {
                    User user = GetUser(tr.UserID);
                    return !user.FirstName.ToLower().Contains(filter.ToLower()) && !user.LastName.ToLower().Contains(filter.ToLower());
                });
            }
            EmployeeController.filter = null;
            return transactions;
        }

        public List<Reservation> AllReservations(string filter) {
            List<Reservation> reservations = Reservations(DateTime.Now);
            if (filter != null) {
                reservations.RemoveAll((Reservation res) => {
                    return !GetSportCentre(res.HallID).Name.ToLower().Contains(filter.ToLower());
                });
            }
            AdminController.filter = null;
            return reservations;
        }

        public List<Transaction> AllTransactions() {
            List<Transaction> transactions = Transactions(LoginController.currentyLoggedPerson);
            return transactions;
        }



        private int GetSportCentreId(string sportCentre) {
            return SportCentre.Where((SportCentre sportCentr) => sportCentr.Name.Equals(sportCentre)).First().SportCentreID;
        }

        public List<Hall> Halls(DateTime dateTime, string sportCentre, string sport) {
            if (dateTime == null || sportCentre == null || sport == null)
                return new List<Hall>();
            int sportCentreID = GetSportCentreId(sportCentre);
            List<Hall> halls = Hall.Where((Hall hall) => sportCentreID.Equals(hall.SportCentreID) && hall.Sport.ToString().Equals(sport)).ToList();
            return halls;
        }

        public List<Hall> Halls(DateTime dateTime, int sportCentreID, string sport) {
            if (dateTime == null || sport == null)
                return new List<Hall>();
            List<Hall> halls = Hall.Where((Hall hall) => sportCentreID.Equals(hall.SportCentreID) && hall.Sport.ToString().Equals(sport)).ToList();
            return halls;
        }

        public List<string> ReservedTimes(List<Hall> halls, bool employee) {
            List<string> reservedTimes = new List<string>();
            DateTime dateTime;
            if (employee)
                dateTime = EmployeeController.selectedDate;
            else
                dateTime = UserController.selectedDate;
            foreach (Hall hall in halls) {
                List<Reservation> reservations = Reservation.Where((Reservation reservation) => reservation.HallID.Equals(hall.HallID) && reservation.DateTime.Date == dateTime.Date).ToList();
                foreach (Reservation reservation in reservations) {
                    reservedTimes.Add(reservation.DateTime.ToShortTimeString());
                }
            }
            return reservedTimes;
        }

        public int GetHallID(string sportCentre, string sport) {
            int sportCentreID = GetSportCentreId(sportCentre);
            return Hall.Where((Hall hall) => sportCentreID.Equals(hall.SportCentreID) && hall.Sport.ToString().Equals(sport)).First().HallID;
        }

        public int GetHallID(int sportCentreID, string sport) {
            return Hall.Where((Hall hall) => sportCentreID.Equals(hall.SportCentreID) && hall.Sport.ToString().Equals(sport)).First().HallID;
        }

        public User GetUser(int userID) {
            return (User)Person.Where((Person person) => person.PersonID.Equals(userID)).First();
        }

        public Person GetPerson(int personID) {
            return Person.Where((Person person) => person.PersonID.Equals(personID)).First();
        }

        public Employee GetEmployee(int userID) {
            return (Employee)Person.Where((Person person) => person.PersonID.Equals(userID)).First();
        }

        public List<User> searchedUsers() {
            List<User> users = Person.Where((Person p) => p is User).OfType<User>().ToList();
            if (AdminController.selectedFilter != null && AdminController.searchInput != null) {
                switch (AdminController.selectedFilter) {
                    case "All users":
                        users = Person.OfType<User>().ToList();
                        return users;
                    case "First Name":
                        users = Person.Where((Person p) => p.FirstName.Equals(AdminController.searchInput)).OfType<User>().ToList();
                        return users;
                    case "Last Name":
                        users = Person.Where((Person p) => p.LastName.Equals(AdminController.searchInput)).OfType<User>().ToList();
                        return users;
                    case "Email":
                        users = Person.Where((Person p) => p.Email.Equals(AdminController.searchInput)).OfType<User>().ToList();
                        return users;
                    case "Username":
                        users = Person.Where((Person p) => p.Username.Equals(AdminController.searchInput)).OfType<User>().ToList();
                        return users;
                }
                AdminController.selectedFilter = null;
                AdminController.searchInput = null;
            }
            return users;
        }
    }
}

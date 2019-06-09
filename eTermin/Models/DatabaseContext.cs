
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

        public SportCentre GetSportCentre(int hallID) {
            return SportCentre.Where((SportCentre sportCentre) => GetHall(hallID).SportCentreID.Equals(sportCentre.SportCentreID)).First();
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

        public List<Hall> Halls() {
            DateTime dateTime = UserController.selectedDate;
            string sportCentre = UserController.selectedSportCentre;
            string sport = UserController.selectedSport;
            if (dateTime == null || sportCentre == null || sport == null)
                return new List<Hall>();
            List<Hall> halls = Hall.Where((Hall hall) => GetSportCentre(hall.HallID).Name.Equals(sportCentre) && hall.Sport.Equals(sport)).ToList();
            return halls;
        }

        public List<Reservation> MyReservations()
        {
            string sportCentre = UserController.selectedSportCentre_MyReservations;
            string sport = UserController.selectedSport_myReservations;
            if (sportCentre != null && sportCentre.Equals("All sport centres")) sportCentre = null;
            if (sport != null && sport.Equals("All sports")) sport = null;
            List<Reservation> reservations = Reservations(LoginController.currentyLoggedPerson);
            if ( sportCentre == null && sport == null )
                return reservations;
            if( sportCentre == null && sport != null)
            {
                for (int i  = 0; i < reservations.Count; i++)
                {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if( !h.Sport.ToString().Equals(sport) )
                    {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            }
            if (sportCentre != null && sport == null)
            {
                SportCentre SC = SportCentre.Where((SportCentre SportCentre) => SportCentre.Name.Equals(sportCentre)).First();
                for (int i = 0; i < reservations.Count; i++)
                {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if (!h.SportCentreID.Equals(SC.SportCentreID))
                    {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            }
            else if( sportCentre != null && sport != null )
            {
                SportCentre sc = SportCentre.Where((SportCentre SportCentre) => SportCentre.Name.Equals(sportCentre)).First();
                for (int i = 0; i < reservations.Count; i++)
                {
                    Hall h = Hall.Where((Hall hall) => hall.HallID.Equals(reservations[i].HallID)).First();
                    if (!h.SportCentreID.Equals(sc.SportCentreID) || !h.Sport.ToString().Equals(sport))
                    {
                        reservations.Remove(reservations[i]);
                        i--;
                    }
                }
            }
            UserController.selectedSportCentre_MyReservations = null;
            UserController.selectedSport_myReservations = null;
            return reservations;
        }
    }
}

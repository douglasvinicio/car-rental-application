using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    class CarsDatabaseContext
    {
        public DbSet<Cars> carsDbset { get; set; }
        public DbSet<Users> usersDbset { get; set; }
        public DbSet<Customers> customersDbset { get; set; }
        public DbSet<Rentals> rentalsDbset { get; set; }
        public DbSet<Returns> returnsDbset { get; set; }        
    }
}

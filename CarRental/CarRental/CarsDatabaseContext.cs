using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    class CarsDatabaseContext : DbContext
    {
        const string DbName = "rentalcardatabase.mdf";

        static string DbPath = Path.Combine(Environment.CurrentDirectory, DbName);
        public CarsDatabaseContext() : base($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={DbPath};Integrated Security=True;Connect Timeout=30")
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Return> Returns { get; set; }        
    }
}

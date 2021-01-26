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
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>().HasMany(c => c.Cars).WithRequired(r => r.Rentals).HasForeignKey(r => r.CarId);
            modelBuilder.Entity<Rental>().HasMany(c => c.Customers).WithRequired(r => r.Rentals).HasForeignKey(r => r.CustomerId);
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
    }
}

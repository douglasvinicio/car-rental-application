using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace CarRental
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salary { get; set; }

        public string Role { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", UserName, Password);
        }      
    }
}

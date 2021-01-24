using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace CarRental
{
    class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }


        [NotMapped]
        public string Password
        {
            get { return Password; }
            set {  Utils.GetPasswordHash(value); }
        }       
    }
}

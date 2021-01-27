using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace CarRental
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        //[StringLength(30)]
        public string UserName { get; set; }


        
        public string Password
        {
            get { return Password; }
            set {  Utils.GetPasswordHash(value); }
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2}", this.EmployeeID, this.UserName, this.Password);
        }

    }
}

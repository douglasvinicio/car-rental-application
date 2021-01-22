using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    [Table("Users")]
    public class Users
    {
        //add the columns
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{UserId}, {UserName},{Password} ";
        }
    }

}

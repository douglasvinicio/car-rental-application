using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class Returns
    {
        //add the columns
        [Key]
        public int ReturnId { get; set; }
        public int CarId { get; set; }

        [Required]
        [StringLength(30)]
        public string CustId { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
        public double Fine { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    class Rental
    {
        public int RentId { get; set; }

        [Required]
        [StringLength(30)]
        public string CarReg { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        [Required]
        public double RentFees { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        public string Phone { get; set; }


        [Required]
        [StringLength(30)]
        public string Status { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental
{
    class Car
    {        
        public int RegNum { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }
        [Required]
        [StringLength(30)]
        public string Model { get; set; }
        [Required]
        [StringLength(30)]
        public string Available { get; set; }
        [Required]
        [StringLength(30)]
        public double Price { get; set; }
        
    }
}

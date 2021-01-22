using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental
{
    [Table("Car")]
    class Car
    {

        //add the columns
        [Key]
        public int CarId { get; set; }

        public string RegNum { get; set; }

        [Required]
        [StringLength(30)]
        public string Brand { get; set; }
        [Required]
        [StringLength(30)]
        public string Model { get; set; }
        [Required]
        [StringLength(30)]
        public string Available { get; set; }
        
        public float FeesPerDay { get; set; }
        public byte[] Photo { get; set; }

        //Single Field- always eagerly loaded
        //public virtual Car car { set; get; }

        public override string ToString()
        {
            return $"{CarId}, {RegNum},{Brand},{Model},{Available},{FeesPerDay},{Photo}";
        }
    }
}

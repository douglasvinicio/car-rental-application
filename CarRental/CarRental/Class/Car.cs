using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental
{
    [Table("Car")]
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Required]
        [StringLength(30)]
        public string RegNum { get; set; }

        [Required]
        [StringLength(30)]
        public string Make { get; set; }

        [Required]
        [StringLength(30)]
        public string Model { get; set; }

        [Required]
        [StringLength(30)]
        public string CarYear { get; set; }

        [Required]
        [StringLength(30)]
        public string CarCategory { get; set; }
        
        [Required]
        public int PassengerCapacity { get; set; }

        public bool AutoTransmission { get; set; }

        public bool BluetoothConn { get; set; }

        public bool IsAvailable { get; set; }


        [Required]
        public float RentalFee { get; set; }


        public byte[] Photo { get; set; }

        //Single Field- always eagerly loaded
        public virtual ICollection<Rental> Rentals { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", Make, Model, CarYear );
        }
    }
}
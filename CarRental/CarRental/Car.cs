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
        [StringLength(30)]
        public string PassCapacity { get; set; }
        [Required]
        [StringLength(30)]
        public string AutoTransmission { get; set; }
        [Required]
        [StringLength(30)]
        public float RentalFee { get; set; }
        [Required]
        [StringLength(30)]
        public string BluetoothConn { get; set; }
        [Required]
        [StringLength(30)]
        public string IsAvailable { get; set; }
        
        public byte[] Photo { get; set; }

        //Single Field- always eagerly loaded
        //public virtual Car car { set; get; }

        public override string ToString()
        {
            return $"{CarId}, {RegNum},{Make},{Model},{CarYear}{CarCategory}{PassCapacity}{AutoTransmission}{RentalFee}{BluetoothConn}{IsAvailable},{Photo}";
        }
    }
}

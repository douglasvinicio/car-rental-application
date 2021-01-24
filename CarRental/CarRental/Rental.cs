using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    [Table("Rental")]
    class Rental
    {
        [Key]
        public int RentId { get; set; }         
        public int CarId { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        [Required]
        public double RentFees { get; set; }

        //Single Field- always eagerly loaded
        public virtual Car car { set; get; }

        public override string ToString()
        {
            return $"{RentId}, {CarId},{RentDate},{RentFees} ";
        }
    }
}

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
    public class Rental
    {
        [Key]
        public int RentalId { get; set; }    
        
        public int CarId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public float TotalFee { get; set; }

        public int TotalDays { get; set; }

        //public string Status {get; set; }
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }
        public enum StatusEnum { Rented = 1, Finalized = 2, Cancelled=3}
        public string Comments { get; set; }

        [NotMapped]
        public float Fine { get; set; }

        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

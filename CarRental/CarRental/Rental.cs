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
        public int RentId { get; set; }    
        
        public int CarId { get; set; }

        public int CustomerId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        public int Discount { get; set; }
        
        public string RentalStatus { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public string DriverLicenseNo { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [StringLength(30)]
        public string City { get; set; }
        [Required]
        [StringLength(30)]
        public string State { get; set; }
        [Required]
        [StringLength(30)]
        public string Country { get; set; }
        [Required]
        [StringLength(30)]
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }

        public override string ToString()
        {
            return $"{CustomerId}, {Name},{Address},{City},{State},{Country},{Email},{Phone}";
        }

    }
}
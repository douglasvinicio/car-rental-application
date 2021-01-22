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
    class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
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
        public string Email {get; set;}
       
       
        public Customer()
        {

        }
        public Customer(int id, string name, string address, string city, string state, string country, string phone, string email)
        {
            this.CustomerId = id;
            this.Name = name;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Email = email;
            this.Phone = phone;
        }
        public override string ToString()
        {
            return $"{CustomerId}, {Name},{Address},{City},{State},{Country},{Email},{Phone}";
        }

    }
}

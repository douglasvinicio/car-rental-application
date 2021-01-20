using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarRental
{

    class Users
    {
        //add the columns
        public int UserId { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }     
    }
        class Customers
    {
        //add the columns
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

    class Cars
    {
        //add the columns
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

//==============================================================================================
    class Rentals
    {
        //add the columns
        public int RentId { get; set; }

        [Required]
        [StringLength(30)]
        public string CarReg { get; set; }
        [Required]      
        public DateTime RentDate { get; set; }
        [Required]
        public double RentFees { get; set; }
       
    }

    //==============================================================================================

    class Returns
    {
        //add the columns
        public int ReturnId { get; set; }

        [Required]
        [StringLength(30)]
        public string CarReg { get; set; }
        [Required]
        public int CustId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        public double Fine { get; set; }
    }

    //==============================================================================================

}

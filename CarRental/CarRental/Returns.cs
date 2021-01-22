using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    [Table("Returns")]
    class Returns
    {
        [Key]
        public int ReturnId { get; set; }

        [Required]
        [StringLength(30)]
        public string CarReg { get; set; }
        
        [Required]
        public int CustId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        public double Fine { get; set; }

        //Single Field- always eagerly loaded
        public virtual Car car { set; get; }
        //Single Field- always eagerly loaded
        public virtual Customer customer { set; get; }


        public override string ToString()
        {
            return $"{ReturnId}, {CarReg},{CustId},{ReturnDate},{Fine} ";
        }
    }
}

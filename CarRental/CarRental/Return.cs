using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    class Return
    {
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
}

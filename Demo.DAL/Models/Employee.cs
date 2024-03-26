using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Enum;

namespace Demo.DAL.Models
{
    
    public class Employee
    {
       
        public int Id{ get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50,ErrorMessage ="Maximum Length of name 50")]
        [MinLength(5, ErrorMessage = "Minimum Length of name 50")]
        public string Name { get; set; }

        //[RegularExpression("^[0-9]{1-3}-[a-zA-Z]{4-10}-[a-zA-Z]{3-10}-[a-zA-Z]{4-10}",ErrorMessage ="Address Must That As 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        public int Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } 

    }
}

using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Maximum Length of name 50")]
        [MinLength(5, ErrorMessage = "Minimum Length of name 50")]
        public string EmpName { get; set; }

        [RegularExpression("^[0-9]{1-3}-[a-zA-Z]{4-10}-[a-zA-Z]{3-10}-[a-zA-Z]{4-10}$",ErrorMessage ="Address Must That As 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        public int Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }


        //forignKey
        [ForeignKey(nameof(department))]
        public int? DeptId { get; set; }

        //Navigational property [one]
        [InverseProperty(nameof(Department.Employees))]
        public virtual Department department { get; set; } = null!;

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
    }
}

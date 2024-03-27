using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        [MaxLength(50)]
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }


        //Navigational Property
        [InverseProperty(nameof(Employee.department))]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

    }
}

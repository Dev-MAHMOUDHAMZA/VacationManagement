using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class Employee:EntityBase
    {
        [Display(Name="Employee Name")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [Display(Name= "Vacation Balance")]
        [Range(1,31)]
        public int VacationBalance { get; set; }

        [Display(Name="Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}

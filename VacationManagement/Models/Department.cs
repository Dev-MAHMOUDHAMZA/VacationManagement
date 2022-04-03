using System.ComponentModel.DataAnnotations;

namespace VacationManagement.Models
{
    public class Department:EntityBase
    {

        [Display(Name ="Department Name")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class RequestVacation:EntityBase
    {
        [Display(Name= "Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Display(Name = "VacationType")]
        public int VacationTypeId { get; set; }
        [ForeignKey("VacationTypeId")]
        public VacationType? VacationType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]

        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]

        public DateTime EndDate { get; set; }
        public string? Comment { get; set; }

        public bool Approved { get; set; }

        public DateTime? DateApproved { get; set; }

        public List<VacationPlan> VacationPlanList { get; set; } = new List<VacationPlan>();

    }
}

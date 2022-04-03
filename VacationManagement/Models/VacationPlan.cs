using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VacationManagement.Models
{
    public class VacationPlan:EntityBase
    {
        [DataType(DataType.Date)]
        [Display(Name= "Vacation Date")]
        [DisplayFormat(DataFormatString="{0:dd-MM-yyyy}")]
        public DateTime? VacationDate { get; set; }

        public int RequestVacationId { get; set; }
        [ForeignKey("RequestVacationId")]
        public RequestVacation? RequestVacation { get; set; }
    }
}

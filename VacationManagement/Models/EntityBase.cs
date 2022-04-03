using System.ComponentModel.DataAnnotations;

namespace VacationManagement.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}

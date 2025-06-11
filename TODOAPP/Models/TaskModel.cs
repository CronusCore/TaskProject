using System.ComponentModel.DataAnnotations;

namespace TODOAPP.Models
{
    public class TaskModel : IValidatableObject
    {
        public int TaskId { get; set; }
        [Required]
        [StringLength(1)]
        [MaxLength(500)]
        public string TaskName { get; set; } = string.Empty;
        [Required]
        public DateTime TaskInitDate { get; set; }
        [Required]
        public DateTime TaskEndDate { get; set; }
        [Required]
        public string TaskDescription { get; set; } = string.Empty;
        public string TaskComments { get; set; } = string.Empty;
        [Required]
        public int EstatusId { get; set; }

        //validación que se ejecutara al momento de utilizar model.isvalid
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.TaskInitDate > this.TaskEndDate)
            {
                yield return new ValidationResult(
                    "Start date cannot be after end date.",
                    new[] { nameof(TaskInitDate), nameof(TaskEndDate) });
            }
        }

    }
}

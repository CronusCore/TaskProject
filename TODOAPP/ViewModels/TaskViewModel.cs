using Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TaskViewModel : IValidatableObject, ITaskBase
    {
        public int TaskId { get; set; } = 0;
        [Required()]
        [MaxLength(500)]
        public string TaskName { get; set; } = string.Empty;
        [Required]
        public DateTime TaskInitDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime TaskEndDate { get; set; } = DateTime.Now.AddDays(1);
        [Required]
        [DisplayName("Description")]
        public string TaskDescription { get; set; } = string.Empty;
        [Required]
        [DisplayName("Comments")]
        public string TaskComments { get; set; } = string.Empty;
        [Required]
        [DisplayName("Estatus")]
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
        
        public List<SelectListItem>? Status { get; set; }
    }
}

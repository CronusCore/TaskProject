using System.ComponentModel.DataAnnotations;

namespace TODOAPP.Models
{
    public class EstatusModel
    {
        public int EstatusId { get; set; }
        public string EstatusName { get; set;} = string.Empty;
        [StringLength(7)]
        public string EstatusColorHexa { get; set; } = string.Empty;
    }
}

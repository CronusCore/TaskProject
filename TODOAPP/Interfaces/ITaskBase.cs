

namespace Interfaces
{
    public interface ITaskBase
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public DateTime TaskInitDate { get; set; }

        public DateTime TaskEndDate { get; set; }
        public string TaskDescription { get; set; } 
        public string TaskComments { get; set; } 
        public int EstatusId { get; set; }
    }
}

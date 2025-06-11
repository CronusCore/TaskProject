namespace DTO
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTime TaskInitDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public string TaskDescription { get; set; }
        public string EstatusColorHexa { get; set; }    
        public string EstatusName { get; set; }
    }
}

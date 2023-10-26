using System.ComponentModel.DataAnnotations;

namespace TaskManagerProject.Models
{
    public class TaskManager
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(100, ErrorMessage = "Task name cannot exceed 100 characters.")]
        public string TaskName { get; set; }
        public DateTime TaskDate { get; set; }
        public string TaskDescription { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int? EstimatedTime { get; set; }
        public int? ActualTime { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
    }

}

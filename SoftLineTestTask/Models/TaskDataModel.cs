using System.Collections.Generic;

namespace SoftLineTestTask.Models
{
    public class TaskDataModel
    {
        public List<Task> Tasks { get; set; }
        
        public bool? IsSuccessDelete { get; set; }
        public bool? IsSuccessEdit { get; set; }
        public bool? IsSuccessAdded { get; set; }
    }
}
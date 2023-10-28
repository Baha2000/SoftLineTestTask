using System.Collections.Generic;

namespace SoftLineTestTask.Models
{
    public class TaskViewModel
    {
        public Task Task { get; set; }

        public IEnumerable<Status> Statuses { get; set; }
    }
}
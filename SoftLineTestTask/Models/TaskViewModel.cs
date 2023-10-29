using System.Collections.Generic;
using SoftLineTestTask.Models.Entity;

namespace SoftLineTestTask.Models
{
    /// <summary>
    /// Модель для отображения задачи во вкладках для взаимодействия с задачей.
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// Задача.
        /// </summary>
        public Task Task { get; set; }

        /// <summary>
        /// Список статусов для отображения на форме.
        /// </summary>
        public IEnumerable<Status> Statuses { get; set; }
    }
}
using System.Collections.Generic;
using SoftLineTestTask.Models.Entity;

namespace SoftLineTestTask.Models
{
    /// <summary>
    /// Модель для отображения задач на главной странице.
    /// </summary>
    public class TasksViewModel
    {
        /// <summary>
        /// Список задач.
        /// </summary>
        public List<Task> Tasks { get; set; }
        
        /// <summary>
        /// Является ли удаление успешным. Если null значит удаления не было.
        /// </summary>
        public bool? IsSuccessDelete { get; set; }
        
        /// <summary>
        /// Является ли редактирование успешным. Если null значит редактирования не было.
        /// </summary>
        public bool? IsSuccessEdit { get; set; }
        
        /// <summary>
        /// Является ли добавление успешным. Если null значит добавления не было.
        /// </summary>
        public bool? IsSuccessAdded { get; set; }
    }
}
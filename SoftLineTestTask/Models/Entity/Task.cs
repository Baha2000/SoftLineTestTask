using System.ComponentModel.DataAnnotations;

namespace SoftLineTestTask.Models.Entity
{
    /// <summary>
    /// Задача.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Идентификатор задачи.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Название задачи.
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Количество символов в названии не может быть менее 4 и более 255", MinimumLength = 4)]
        public string Name { get; set; }
        
        /// <summary>
        /// Описание задачи.
        /// </summary>
        [Required]
        [StringLength(255, ErrorMessage = "Количество символов в описании не может быть менее 4 и более 255", MinimumLength = 4)]
        public string Description { get; set; }
        
        /// <summary>
        /// Идентификатор статуса.
        /// </summary>
        [Required]
        public int StatusId { get; set; }
        
        /// <summary>
        /// Статус задачи.
        /// </summary>
        public virtual Status Status { get; set; }
    }
}
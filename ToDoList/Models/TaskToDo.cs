using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class TaskToDo
    {

        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }

        [Required]

        public DateTime? Date { get; set; }

        public int IsCompleted { get; set; }
        public int priority { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}

namespace ToDoList.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<TaskToDo> tasks { get; set; }
    }
}

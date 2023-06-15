namespace ToDoList.Models
{
    public class Users
    {
        public int Id { get; set; }
        // [Required]
        public string fName { get; set; }
        public string lName { get; set; }
        public string email { get; set; }
        public string Password { get; set; }

        public char IsAdmin { get; set; }



    }
}

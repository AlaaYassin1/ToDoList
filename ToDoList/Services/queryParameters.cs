namespace ToDoList.Services
{
    public class queryParameters
    {
        public string SortBy { get; set; } = "Id";

        string _sortOrder = "asc";
        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == "asc" || value == "desc")
                {
                    _sortOrder = value;
                }
            }
        }
    }
}

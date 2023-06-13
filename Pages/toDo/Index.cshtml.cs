using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Pages.toDo
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public TaskToDo task { get; set; }

        [BindProperty(SupportsGet = true)]
        public TaskQueryParameter taskQueryParameter { get; set; }

        public List<Category> categories { get; set; } = List.GetCategory();

        public List<SelectListItem> selectedcategory { get; set; } = default!;

        public List<TaskToDo>? tasks { get; set; }
        public void OnGet()
        {
            selectedcategory = categories.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
            // tasks = List.GetData(1);
            //  tasks = List.SortOrder(taskQueryParameter.SortOrder);
            // categories = List.GetCategory();
            tasks = List.filterByCategory(taskQueryParameter.categoryID, taskQueryParameter.SortOrder);
        }
        public async Task<IActionResult> OnPostEditAsync()
        {
            List.UpdateData(2, task);
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostAddAsync()
        {

            List.InsertData(2, task);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            List.DeleteData(id);
            return RedirectToPage();
        }

    }
}

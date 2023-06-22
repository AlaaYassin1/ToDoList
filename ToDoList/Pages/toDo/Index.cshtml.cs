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
        //public Users CurrentUser { get; set; }
        public List<TaskToDo> tasks { get; set; }

        public QueryPageResult PageResult { get; set; }
        public IEnumerable<SelectListItem> lstPageSize { get; set; } =
            new List<SelectListItem>() {
            new SelectListItem(){Value="5",Text="5" },
            new SelectListItem(){Value="10",Text="10" },
            new SelectListItem(){Value="20",Text="20" },
        };
        //id ==1
        public int pageId { get; set; } = 1;
        public void OnGet()
        {


            int result = authFun.check_Permission(pageId);

            selectedcategory = categories.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name }).ToList();
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

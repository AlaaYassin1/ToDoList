using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;

namespace ToDoList.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {

            authFun.currentUser = null;
            return RedirectToPage("/Index");
        }
    }
}

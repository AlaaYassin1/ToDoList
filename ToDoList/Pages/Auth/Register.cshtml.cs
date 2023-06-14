using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputRegisterVM inputRegisterVM { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int result = authFun.InsertData(inputRegisterVM);
            if (result == 0)
            {
                ModelState.AddModelError(string.Empty, "The email is exist");
                return Page();
            }

            return RedirectToPage("/Auth/Login");
        }

    }
}

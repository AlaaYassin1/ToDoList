using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputLoginVM inputLoginVM { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Users result = authFun.LoginUser(inputLoginVM);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "The email address or password you entered is invalid");
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}

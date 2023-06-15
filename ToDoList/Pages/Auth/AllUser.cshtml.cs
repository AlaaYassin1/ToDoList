using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoList.Models;

namespace ToDoList.Pages.Auth
{
    public class AllUserModel : PageModel
    {

        public List<Users> userslst { get; set; }
        public List<UserRole> roleList { get; set; }
        public void OnGet()
        {

            userslst = authFun.GetData();
            roleList = authFun.getPermissions();
        }
    }
}

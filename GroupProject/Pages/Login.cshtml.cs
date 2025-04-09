using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TuesdayCore.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string EmailOrPhone { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            // Basic validation for demonstration purposes
            if (string.IsNullOrEmpty(EmailOrPhone) || string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Please fill in all fields.";
                return Page();
            }

            // Add your authentication logic here (e.g., check credentials against a database)
            // For now, we'll redirect to a hypothetical dashboard page on successful login
            return RedirectToPage("/Dashboard", new { area = "" }); ; // Replace with your actual redirect page
        }
    }
}
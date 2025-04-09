using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // This method is called when the page is loaded
        }

        public IActionResult OnPostSkip()
        {
            // Redirect to the Login page when SKIP is clicked
            return RedirectToPage("/Login");
        }
    }
}
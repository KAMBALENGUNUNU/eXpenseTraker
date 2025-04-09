using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ExpenseTracker.Pages
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string IconClass { get; set; }
        public string IconBackground { get; set; }
    }

    public class BudgetModel : PageModel
    {
        public List<CategoryViewModel> Categories { get; set; }

        [BindProperty]
        public string CategoryName { get; set; }

        [BindProperty]
        public decimal CategoryBudget { get; set; }

        [BindProperty]
        public string IconClass { get; set; }

        public void OnGet()
        {
            // In a real application, you would load these from your database
            Categories = new List<CategoryViewModel>
            {
                new CategoryViewModel {
                    Id = 1,
                    Name = "Rent",
                    Amount = 620.3m,
                    IconClass = "fas fa-building",
                    IconBackground = "#e3f2fd"
                },
                new CategoryViewModel {
                    Id = 2,
                    Name = "Groceries",
                    Amount = 115.5m,
                    IconClass = "fas fa-user",
                    IconBackground = "#e8f5e9"
                },
                new CategoryViewModel {
                    Id = 3,
                    Name = "School loan",
                    Amount = 650.5m,
                    IconClass = "fas fa-graduation-cap",
                    IconBackground = "#fff3e0"
                },
                new CategoryViewModel {
                    Id = 4,
                    Name = "Shopping",
                    Amount = 21.5m,
                    IconClass = "fas fa-shopping-bag",
                    IconBackground = "#fce4ec"
                },
                new CategoryViewModel {
                    Id = 5,
                    Name = "Others",
                    Amount = 21.5m,
                    IconClass = "fas fa-ellipsis-h",
                    IconBackground = "#e0f2f1"
                }
            };
        }

        public IActionResult OnPost()
        {
            // In a real application, you would save the new category to your database
            // For now, we'll just redirect back to the budget page
            return RedirectToPage("Budget");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Pages
{
    public class ReportsModel : PageModel
    {
        public string UserProfileImage { get; set; }
        public List<TransactionItem> Transactions { get; set; }
        public decimal TotalExpenses { get; private set; }
        public string TopCategory { get; private set; }
        public decimal TopCategoryAmount { get; private set; }
        public decimal AverageExpense { get; private set; }

        public void OnGet()
        {
            // Set user profile image
            UserProfileImage = "/images/user-profile.jpg";

            // Initialize transactions list - using existing TransactionItem class
            Transactions = new List<TransactionItem>
            {
                new TransactionItem
                {
                    Id = 1,
                    Category = "Rent",
                    Code = "10",
                    Price = 121.00M,
                    Status = "Lower",
                    Added = "29 Dec 2022",
                    IsChecked = true
                },
                new TransactionItem
                {
                    Id = 2,
                    Category = "Shopping",
                    Code = "204",
                    Price = 590.00M,
                    Status = "Higher",
                    Added = "24 Dec 2022",
                    IsChecked = true
                },
                new TransactionItem
                {
                    Id = 3,
                    Category = "Groceries",
                    Code = "48",
                    Price = 125.00M,
                    Status = "Average",
                    Added = "12 Dec 2022",
                    IsChecked = false
                },
                new TransactionItem
                {
                    Id = 4,
                    Category = "Others",
                    Code = "401",
                    Price = 348.00M,
                    Status = "Higher",
                    Added = "21 Oct 2022",
                    IsChecked = false
                },
                new TransactionItem
                {
                    Id = 5,
                    Category = "Others",
                    Code = "432",
                    Price = 284.00M,
                    Status = "Higher",
                    Added = "21 Oct 2022",
                    IsChecked = false
                },
                new TransactionItem
                {
                    Id = 6,
                    Category = "Others",
                    Code = "0",
                    Price = 760.00M,
                    Status = "Lower",
                    Added = "19 Sep 2022",
                    IsChecked = false
                },
                new TransactionItem
                {
                    Id = 7,
                    Category = "Groceries",
                    Code = "347",
                    Price = 400.00M,
                    Status = "Higher",
                    Added = "19 Sep 2022",
                    IsChecked = false
                }
            };

            // Calculate metrics
            CalculateMetrics();
        }

        private void CalculateMetrics()
        {
            // Calculate total expenses
            TotalExpenses = Transactions.Sum(t => t.Price);

            // Calculate average expense
            AverageExpense = Transactions.Count > 0 ? TotalExpenses / Transactions.Count : 0;

            // Find top spending category
            var categoryGroups = Transactions
                .GroupBy(t => t.Category)
                .Select(g => new { Category = g.Key, TotalAmount = g.Sum(t => t.Price) })
                .OrderByDescending(g => g.TotalAmount)
                .ToList();

            if (categoryGroups.Any())
            {
                var topCategoryGroup = categoryGroups.First();
                TopCategory = topCategoryGroup.Category;
                TopCategoryAmount = topCategoryGroup.TotalAmount;
            }
            else
            {
                TopCategory = "None";
                TopCategoryAmount = 0;
            }
        }
    }

    // TransactionItem class is removed from here since it's already defined in the namespace
}
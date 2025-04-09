using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Pages
{
    public class DashboardModel : PageModel
    {
        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal MonthlyBudget { get; set; }
        public decimal BudgetRemaining { get; set; }
        public int DailyAverage { get; set; }

        public List<Category> Categories { get; set; }
        public decimal QuickTransferAmount { get; set; }
        public string QuickTransferFrom { get; set; }
        public string QuickTransferTo { get; set; }
        public List<DailyTransaction> DailyTransactions { get; set; }

        public void OnGet()
        {
            // User profile
            UserProfileImage = "/Images/profile.jpg";
            UserName = "PNRDM GROUP";

            // Financial data (matching the HTML)
            MonthlyIncome = 3258.00M;
            MonthlyExpenses = 2246.00M;
            TotalSavings = 3358.00M;
            MonthlyBudget = 1500.00M;
            BudgetRemaining = 750.00M;

            // Daily average for gauge chart
            DailyAverage = 120;

            // Categories (matching the HTML)
            Categories = new List<Category>
            {
                new Category { Name = "Food", Amount = 450, ColorClass = "category-green" },
                new Category { Name = "Rent", Amount = 850, ColorClass = "category-red" },
                new Category { Name = "Utilities", Amount = 120, ColorClass = "category-orange" }
            };

            // Quick transfer data (matching the HTML)
            QuickTransferAmount = 50.00M;
            QuickTransferFrom = "Personal Checking";
            QuickTransferTo = "Savings Account";

            // Daily transactions for bar chart (matching the HTML)
            DailyTransactions = new List<DailyTransaction>
            {
                new DailyTransaction { Date = "Sept 10", Amount = 100 },
                new DailyTransaction { Date = "Sept 11", Amount = 80 },
                new DailyTransaction { Date = "Sept 12", Amount = 60 },
                new DailyTransaction { Date = "Sept 13", Amount = 110 },
                new DailyTransaction { Date = "Sept 14", Amount = 40 },
                new DailyTransaction { Date = "Sept 15", Amount = 20 },
                new DailyTransaction { Date = "Sept 16", Amount = 70 },
                new DailyTransaction { Date = "Sept 17", Amount = 10 },
                new DailyTransaction { Date = "Sept 18", Amount = 50 },
                new DailyTransaction { Date = "Sept 19", Amount = 30 },
                new DailyTransaction { Date = "Sept 20", Amount = 40 },
                new DailyTransaction { Date = "Sept 21", Amount = 20 },
                new DailyTransaction { Date = "Sept 22", Amount = 50 },
                new DailyTransaction { Date = "Sept 23", Amount = 10 },
                new DailyTransaction { Date = "Sept 24", Amount = 40 }
            };
        }
    }

    public class Category
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string ColorClass { get; set; }
    }

    public class DailyTransaction
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.Pages
{
    public class DashboardModel : PageModel
    {
        public string UserProfileImage { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlyExpenses { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal SavingGoals { get; set; }
        public decimal IncomeDifference { get; set; }
        public decimal ExpensesDifference { get; set; }
        public decimal BalanceDifference { get; set; }
        public decimal SavingGoalsDifference { get; set; }

        public List<Transaction> RecentTransactions { get; set; }
        public List<SavingsData> SavingsData { get; set; }
        public int TotalAttendance { get; set; }
        public Dictionary<string, int> AttendanceStatus { get; set; }
        public List<DailyTransaction> DailyTransactions { get; set; }

        public void OnGet()
        {
            // Set the user profile image
            UserProfileImage = "/images/user-profile.jpg";

            // Set financial data
            MonthlyIncome = 3245.00M;
            MonthlyExpenses = 2246.00M;
            CurrentBalance = 330.00M;
            SavingGoals = 550.00M;

            // Calculate differences (5% in this case)
            IncomeDifference = 5.0M;
            ExpensesDifference = 5.0M;
            BalanceDifference = 5.0M;
            SavingGoalsDifference = 5.0M;

            // Recent transactions
            RecentTransactions = new List<Transaction>
            {
                new Transaction {
                    Name = "Rent",
                    Amount = 400.00M,
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Housing",
                    IconClass = "fas fa-home",
                    ColorClass = "green"
                },
                new Transaction {
                    Name = "Groceries",
                    Amount = 200.00M,
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Food",
                    IconClass = "fas fa-shopping-basket",
                    ColorClass = "red"
                },
                new Transaction {
                    Name = "Others",
                    Amount = 20.00M,
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Miscellaneous",
                    IconClass = "fas fa-ellipsis-h",
                    ColorClass = "orange"
                },
                new Transaction {
                    Name = "Others",
                    Amount = 15.00M,
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Miscellaneous",
                    IconClass = "fas fa-ellipsis-h",
                    ColorClass = "orange"
                }
            };

            // Savings data for chart
            SavingsData = new List<SavingsData>
            {
                new SavingsData { Date = "Mar 5", Amount = 300 },
                new SavingsData { Date = "Mar 10", Amount = 400 },
                new SavingsData { Date = "Mar 15", Amount = 350 },
                new SavingsData { Date = "Mar 20", Amount = 450 },
                new SavingsData { Date = "Mar 25", Amount = 300 },
                new SavingsData { Date = "Mar 30", Amount = 550 }
            };

            // Attendance data
            TotalAttendance = 120;
            AttendanceStatus = new Dictionary<string, int>
            {
                { "Average", 75 },
                { "Higher", 15 },
                { "Lower", 10 },
                { "Others", 10 }
            };

            // Daily transactions for bar chart
            DailyTransactions = new List<DailyTransaction>
            {
                new DailyTransaction { Date = "Sept 10", Amount = 40 },
                new DailyTransaction { Date = "Sept 11", Amount = 60 },
                new DailyTransaction { Date = "Sept 12", Amount = 70 },
                new DailyTransaction { Date = "Sept 13", Amount = 30 },
                new DailyTransaction { Date = "Sept 14", Amount = 20 },
                new DailyTransaction { Date = "Sept 15", Amount = 35 },
                new DailyTransaction { Date = "Sept 16", Amount = 35 },
                new DailyTransaction { Date = "Sept 17", Amount = 35 },
                new DailyTransaction { Date = "Sept 18", Amount = 35 },
                new DailyTransaction { Date = "Sept 19", Amount = 35 },
                new DailyTransaction { Date = "Sept 20", Amount = 35 },
                new DailyTransaction { Date = "Sept 21", Amount = 35 },
                new DailyTransaction { Date = "Sept 22", Amount = 45 }
            };
        }
    }

    public class Transaction
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string IconClass { get; set; }
        public string ColorClass { get; set; }
    }

    public class SavingsData
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class DailyTransaction
    {
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }
}
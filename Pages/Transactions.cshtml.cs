using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ExpenseTracker.Pages
{
    public class TransactionsModel : PageModel
    {
        public string UserProfileImage { get; set; }
        public List<TransactionItem> Transactions { get; set; }

        public void OnGet()
        {
            // Set user profile image
            UserProfileImage = "/images/user-profile.jpg";

            // Initialize transactions list
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
        }
    }

    public class TransactionItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string Added { get; set; }
        public bool IsChecked { get; set; }
    }
}
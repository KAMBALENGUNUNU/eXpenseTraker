using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Pages
{
    public class ReportsModel : PageModel
    {
        private readonly string _connectionString = "Data Source=DESKTOP-DM4DTSD;Initial Catalog=Tracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<string> Categories { get; set; }
        public decimal TotalExpenses { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            // User profile data
            UserProfileImage = "/Images/profile.jpg";
            UserName = "PNRDM GROUP";

            Transactions = new List<Transaction>();
            Categories = new List<string>();
            TotalExpenses = 0;

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Fetch unique categories for the filter dropdown
                    string categoryQuery = "SELECT DISTINCT Category FROM Transactions";
                    using (Microsoft.Data.SqlClient.SqlCommand categoryCmd = new Microsoft.Data.SqlClient.SqlCommand(categoryQuery, conn))
                    {
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = categoryCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categories.Add(reader.GetString(0));
                            }
                        }
                    }

                    // Build the query for fetching transactions
                    string query = @"
                        SELECT Id, Category, Code, Price, Status, AddedDate 
                        FROM Transactions 
                        WHERE 1=1";
                    if (!string.IsNullOrEmpty(SearchTerm))
                    {
                        query += " AND (Category LIKE @SearchTerm OR CAST(Code AS NVARCHAR) LIKE @SearchTerm)";
                    }
                    if (!string.IsNullOrEmpty(FilterCategory))
                    {
                        query += " AND Category = @FilterCategory";
                    }
                    if (FilterDate.HasValue)
                    {
                        query += " AND CAST(AddedDate AS DATE) = @FilterDate";
                    }
                    query += " ORDER BY AddedDate DESC";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(SearchTerm))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{SearchTerm}%");
                        }
                        if (!string.IsNullOrEmpty(FilterCategory))
                        {
                            cmd.Parameters.AddWithValue("@FilterCategory", FilterCategory);
                        }
                        if (FilterDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FilterDate", FilterDate.Value.Date);
                        }

                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var transaction = new Transaction
                                {
                                    Id = reader.GetInt32(0),
                                    Category = reader.GetString(1),
                                    Code = reader.GetInt32(2),
                                    Price = reader.GetDecimal(3),
                                    Status = reader.GetString(4),
                                    AddedDate = reader.GetDateTime(5)
                                };
                                Transactions.Add(transaction);
                                TotalExpenses += transaction.Price;
                            }
                        }
                    }

                    // If no transactions are found, set a message
                    if (Transactions.Count == 0)
                    {
                        ErrorMessage = "No transactions found for the selected filters.";
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = $"Database error: {ex.Message}";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            }
        }

        public IActionResult OnPostExportToCsv()
        {
            // Ensure Transactions is populated (re-run the query if necessary)
            OnGet(); // This re-populates the Transactions list with the current filters

            if (Transactions == null || Transactions.Count == 0)
            {
                ErrorMessage = "No transactions available to export.";
                return Page();
            }

            // Create CSV content
            var csv = new StringBuilder();

            // Add header
            csv.AppendLine("#,Category,Code,Price,Status,Added");

            // Add data rows
            for (int i = 0; i < Transactions.Count; i++)
            {
                var transaction = Transactions[i];
                // Escape commas in fields to prevent CSV formatting issues
                string category = transaction.Category.Replace(",", ""); // Simple escaping by removing commas
                string price = transaction.Price.ToString("N2"); // Format price with 2 decimal places
                string addedDate = transaction.AddedDate.ToString("dd MMM yyyy");
                string status = transaction.Status;

                csv.AppendLine($"{i + 1},{category},{transaction.Code},{price},{status},{addedDate}");
            }

            // Convert CSV content to bytes
            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());

            // Return the file for download
            return File(csvBytes, "text/csv", "TransactionsReport.csv");
        }
    }

    public class Transaction
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int Code { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
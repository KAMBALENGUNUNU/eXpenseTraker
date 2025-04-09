using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Project.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly string _connectionString = "Data Source=TAGLT169\\SQLEXPRESS;Initial Catalog=Tracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public string UserProfileImage { get; set; }
        public string UserName { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);

        [BindProperty]
        public Transaction NewTransaction { get; set; } = new Transaction(); // Initialize to prevent null

        [BindProperty]
        public Transaction EditTransaction { get; set; } = new Transaction(); // Initialize to prevent null

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet(int page = 1)
        {
            // User profile data
            UserProfileImage = "/images/user-profile.jpg";
            UserName = "Prisca Nova";

            // Pagination settings
            CurrentPage = page;
            ItemsPerPage = 7; // Matches the "Showing 1-7" in the HTML

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Build the query for counting total items
                    string countQuery = "SELECT COUNT(*) FROM Transactions WHERE 1=1";
                    if (!string.IsNullOrEmpty(SearchTerm))
                    {
                        countQuery += " AND (Category LIKE @SearchTerm OR CAST(Code AS NVARCHAR) LIKE @SearchTerm)";
                    }
                    if (!string.IsNullOrEmpty(FilterStatus) && FilterStatus != "All Expenses")
                    {
                        countQuery += " AND Status = @FilterStatus";
                    }
                    if (FilterDate.HasValue)
                    {
                        countQuery += " AND CAST(AddedDate AS DATE) = @FilterDate";
                    }

                    using (Microsoft.Data.SqlClient.SqlCommand countCmd = new Microsoft.Data.SqlClient.SqlCommand(countQuery, conn))
                    {
                        if (!string.IsNullOrEmpty(SearchTerm))
                        {
                            countCmd.Parameters.AddWithValue("@SearchTerm", $"%{SearchTerm}%");
                        }
                        if (!string.IsNullOrEmpty(FilterStatus) && FilterStatus != "All Expenses")
                        {
                            countCmd.Parameters.AddWithValue("@FilterStatus", FilterStatus);
                        }
                        if (FilterDate.HasValue)
                        {
                            countCmd.Parameters.AddWithValue("@FilterDate", FilterDate.Value.Date);
                        }
                        TotalItems = (int)countCmd.ExecuteScalar();
                    }

                    // Build the query for fetching transactions with pagination
                    string query = @"
                        SELECT Id, Category, Code, Price, Status, AddedDate 
                        FROM Transactions 
                        WHERE 1=1";
                    if (!string.IsNullOrEmpty(SearchTerm))
                    {
                        query += " AND (Category LIKE @SearchTerm OR CAST(Code AS NVARCHAR) LIKE @SearchTerm)";
                    }
                    if (!string.IsNullOrEmpty(FilterStatus) && FilterStatus != "All Expenses")
                    {
                        query += " AND Status = @FilterStatus";
                    }
                    if (FilterDate.HasValue)
                    {
                        query += " AND CAST(AddedDate AS DATE) = @FilterDate";
                    }
                    query += " ORDER BY AddedDate DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(SearchTerm))
                        {
                            cmd.Parameters.AddWithValue("@SearchTerm", $"%{SearchTerm}%");
                        }
                        if (!string.IsNullOrEmpty(FilterStatus) && FilterStatus != "All Expenses")
                        {
                            cmd.Parameters.AddWithValue("@FilterStatus", FilterStatus);
                        }
                        if (FilterDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FilterDate", FilterDate.Value.Date);
                        }
                        cmd.Parameters.AddWithValue("@Offset", (CurrentPage - 1) * ItemsPerPage);
                        cmd.Parameters.AddWithValue("@PageSize", ItemsPerPage);

                        Transactions = new List<Transaction>();
                        using (Microsoft.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Transactions.Add(new Transaction
                                {
                                    Id = reader.GetInt32(0),
                                    Category = reader.GetString(1),
                                    Code = reader.GetInt32(2),
                                    Price = reader.GetDecimal(3),
                                    Status = reader.GetString(4),
                                    AddedDate = reader.GetDateTime(5)
                                });
                            }
                        }
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

        public IActionResult OnPostAddExpense()
        {
            if (string.IsNullOrEmpty(NewTransaction.Category) || NewTransaction.Code == 0 ||
                NewTransaction.Price == 0 || string.IsNullOrEmpty(NewTransaction.Status))
            {
                ErrorMessage = "Please fill in all fields.";
                return Page();
            }

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    string query = @"
                        INSERT INTO Transactions (Category, Code, Price, Status, AddedDate) 
                        VALUES (@Category, @Code, @Price, @Status, @AddedDate)";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", NewTransaction.Category);
                        cmd.Parameters.AddWithValue("@Code", NewTransaction.Code);
                        cmd.Parameters.AddWithValue("@Price", NewTransaction.Price);
                        cmd.Parameters.AddWithValue("@Status", NewTransaction.Status);
                        cmd.Parameters.AddWithValue("@AddedDate", NewTransaction.AddedDate);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            SuccessMessage = "Transaction added successfully!";
                            return RedirectToPage("/Transactions");
                        }
                        else
                        {
                            ErrorMessage = "Failed to add transaction. Please try again.";
                            return Page();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = $"Database error: {ex.Message}";
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                return Page();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Transactions WHERE Id = @Id";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            SuccessMessage = "Transaction deleted successfully!";
                        }
                        else
                        {
                            ErrorMessage = "Transaction not found.";
                        }
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

            return RedirectToPage("/Transactions", new { page = CurrentPage });
        }

        public IActionResult OnPostEdit()
        {
            if (string.IsNullOrEmpty(EditTransaction.Category) || EditTransaction.Code == 0 ||
                EditTransaction.Price == 0 || string.IsNullOrEmpty(EditTransaction.Status))
            {
                ErrorMessage = "Please fill in all fields.";
                return Page();
            }

            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
                {
                    string query = @"
                        UPDATE Transactions 
                        SET Category = @Category, Code = @Code, Price = @Price, Status = @Status, AddedDate = @AddedDate 
                        WHERE Id = @Id";
                    using (Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", EditTransaction.Id);
                        cmd.Parameters.AddWithValue("@Category", EditTransaction.Category);
                        cmd.Parameters.AddWithValue("@Code", EditTransaction.Code);
                        cmd.Parameters.AddWithValue("@Price", EditTransaction.Price);
                        cmd.Parameters.AddWithValue("@Status", EditTransaction.Status);
                        cmd.Parameters.AddWithValue("@AddedDate", EditTransaction.AddedDate);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            SuccessMessage = "Transaction updated successfully!";
                        }
                        else
                        {
                            ErrorMessage = "Transaction not found.";
                        }
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

            return RedirectToPage("/Transactions", new { page = CurrentPage });
        }
    }

    public class Transactions
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int Code { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
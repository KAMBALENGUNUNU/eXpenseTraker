using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
        private readonly string _connectionString = "Data Source=TAGLT169\\SQLEXPRESS;Initial Catalog=Tracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        [BindProperty]
        public string CategoryName { get; set; }

        [BindProperty]
        public decimal CategoryBudget { get; set; }

        [BindProperty]
        public string IconClass { get; set; }

        [BindProperty]
        public string IconBackground { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            Categories.Clear();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT Id, Name, Amount, IconClass, IconBackground FROM BudgetCategories";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categories.Add(new CategoryViewModel
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Amount = reader.GetDecimal(2),
                                    IconClass = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    IconBackground = reader.IsDBNull(4) ? "" : reader.GetString(4)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = "SQL error: " + ex.Message;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Unexpected error: " + ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(CategoryName) || CategoryBudget <= 0)
            {
                ErrorMessage = "Please enter valid data.";
                OnGet(); // Refresh categories
                return Page();
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO BudgetCategories (Name, Amount, IconClass, IconBackground) VALUES (@Name, @Amount, @IconClass, @IconBackground)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", CategoryName);
                        cmd.Parameters.AddWithValue("@Amount", CategoryBudget);
                        cmd.Parameters.AddWithValue("@IconClass", IconClass ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IconBackground", IconBackground ?? (object)DBNull.Value);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            SuccessMessage = "Category added successfully!";
                            return RedirectToPage("Budget");
                        }
                        else
                        {
                            ErrorMessage = "Insert failed. Please try again.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMessage = "SQL error: " + ex.Message;
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Unexpected error: " + ex.Message;
            }

            OnGet(); // Reload categories on error
            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TuesdayCore.Pages
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public string Names { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            // This method is called when the page is loaded
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash returns a byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Convert each byte to a hexadecimal string
                }
                return builder.ToString();
            }
        }

        public IActionResult OnPost()
        {
            // Basic validation
            if (string.IsNullOrEmpty(Names) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(ConfirmPassword))
            {
                ErrorMessage = "Please fill in all fields.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            // Hash the password
            string hashedPassword = HashPassword(Password);

            // Save to database
            try
            {
                string connectionString = "Data Source=TAGLT169\\SQLEXPRESS;Initial Catalog=Tracker;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Users (Names, Email, Phone, Password) 
                                   VALUES (@Names, @Email, @Phone, @Password)";
                    Microsoft.Data.SqlClient.SqlCommand cmd = new Microsoft.Data.SqlClient.SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Names", Names);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        SuccessMessage = "Registration Successful!";
                        return RedirectToPage("/Login");
                    }
                    else
                    {
                        ErrorMessage = "Registration failed. Please try again.";
                        return Page();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627) // Unique constraint violation
            {
                ErrorMessage = "An account with this email already exists.";
                return Page();
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
    }
}
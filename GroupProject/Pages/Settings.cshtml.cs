using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Pages.Settings
{
    public class IndexModel : PageModel
    {
        public string UserProfileImage { get; private set; }
        [BindProperty]
        public UserProfileViewModel UserProfile { get; set; }

        public List<SelectListItem> AvailableLanguages { get; set; }

        public IndexModel()
        {
            // Initialize languages
            AvailableLanguages = new List<SelectListItem>
            {
                new SelectListItem { Value = "en", Text = "English" },
                new SelectListItem { Value = "fr", Text = "French" },
                new SelectListItem { Value = "es", Text = "Spanish" },
                new SelectListItem { Value = "de", Text = "German" }
            };
        }

        public void OnGet()
        {
            // In a real application, you would fetch this data from your database
            // This is just sample data to populate the view
           
            UserProfile = new UserProfileViewModel

            {
                Name = "PNRDM GROUP",
                Email = "PNRDMGROUP@gmail.com",
                ProfileImageUrl = "/Images/profile.jpg", // Update with your actual path
                PhoneNumber = "+25070009346",
                Address = "KG 48 st",
                City = "Kigali city",
                DateOfBirth = new DateTime(2000, 5, 5),
                NationalId = "19908012873005",
                JoinedAt = new DateTime(2024, 6, 29),
                DarkTheme = false,
                Language = "en"
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // In a real application, you would save changes to the database
            // For now, we'll just redirect back to the page
            return RedirectToPage();
        }
    }

    public class UserProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool DarkTheme { get; set; }
        public string Language { get; set; }
    }
}
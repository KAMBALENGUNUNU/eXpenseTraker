using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTracker.Services
{
    public class ExpenseService
    {
        private readonly ApplicationDbContext _context;

        public ExpenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetUserExpensesAsync(string userId)
        {
            return await _context.Expenses
                //.Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetCurrentUserExpensesAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return new List<Expense>();

            return await GetUserExpensesAsync(userId);
        }

        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            expense.Date = DateTime.SpecifyKind(expense.Date, DateTimeKind.Utc);
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }
    }
}


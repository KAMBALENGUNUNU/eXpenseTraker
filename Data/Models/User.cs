// User.cs
public class User
{
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsGoogleAuth { get; set; }

    // Navigation properties
    public ICollection<Expense> Expenses { get; set; }
}

// Category.cs
public class Category
{
    public int CategoryID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDefault { get; set; }

    // Navigation properties
    public ICollection<Expense> Expenses { get; set; }
}

// Expense.cs
public class Expense
{
    public int ExpenseID { get; set; }
    public int UserID { get; set; }
    public int CategoryID { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Modified { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Category Category { get; set; }
}
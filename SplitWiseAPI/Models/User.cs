namespace SplitWiseAPI.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Expense> ExpensesPaid { get; set; } = new();   // Expenses user paid
        public List<Expense> ExpensesSplit { get; set; } = new();
        public List<Group> Groups { get; set; } = new();
    }
}

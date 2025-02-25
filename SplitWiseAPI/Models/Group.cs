namespace SplitWiseAPI.Models
{
    public class Group
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
    }
}

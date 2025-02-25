namespace SplitWiseAPI.Models
{
    public class Expense
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public Guid PaidByUserId { get; set; }   // Foreign key for User
        public User PaidBy { get; set; }         // Navigation property

        public List<User> SplitAmong { get; set; } = new(); // Split users

        public Guid GroupId { get; set; }        // Foreign key for Group
        public Group Group { get; set; }         // Navigation property
    }
}

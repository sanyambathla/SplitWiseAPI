using SplitWiseAPI.Models;

namespace SplitWiseAPI.DTOs
{
    public class ExpenseCreateDTO
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid PaidByUserId { get; set; }
        public List<Guid> SplitAmongUserIds { get; set; }
        public Guid GroupId { get; set; }
    }

    public class ExpenseResponseDTO
    {
        public ExpenseResponseDTO() { }

        public ExpenseResponseDTO(Expense expense)
        {
            Id = expense.Id;
            Description = expense.Description;
            Amount = expense.Amount;
            PaidBy = new UserResponseDTO(expense.PaidBy);
            SplitAmong = expense.SplitAmong.Select(u => new UserResponseDTO(u)).ToList();
        }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public UserResponseDTO PaidBy { get; set; }
        public List<UserResponseDTO> SplitAmong { get; set; }
        public Guid GroupId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ExpenseUpdateDTO
    {
        public Guid Id { get; set; }                     // Expense ID (required for updating)
        public string Description { get; set; } = null!; // Updated description
        public decimal Amount { get; set; }              // Updated amount
        public Guid PaidByUserId { get; set; }           // Updated payer's user ID
        public List<Guid> SplitAmongUserIds { get; set; } = new(); // Updated list of user IDs to split among
    }

}

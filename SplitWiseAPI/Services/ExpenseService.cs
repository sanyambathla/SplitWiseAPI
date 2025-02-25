using Microsoft.EntityFrameworkCore;
using SplitWiseAPI.DbContext;
using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly SplitwiseDbContext _context;

        public ExpenseService(SplitwiseDbContext context)
        {
            _context = context;
        }

        public async Task<Expense?> AddExpenseAsync(Guid groupId, Expense expense)
        {
            var group = await _context.Groups.Include(g => g.Expenses).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null) return null;

            group.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> UpdateExpenseAsync(Guid groupId, Expense expense)
        {
            var group = await _context.Groups.Include(g => g.Expenses).FirstOrDefaultAsync(g => g.Id == groupId);
            var existingExpense = group?.Expenses.FirstOrDefault(e => e.Id == expense.Id);

            if (existingExpense == null) return false;

            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            existingExpense.PaidBy = expense.PaidBy;
            existingExpense.SplitAmong = expense.SplitAmong;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(Guid groupId, Guid expenseId)
        {
            var group = await _context.Groups.Include(g => g.Expenses).FirstOrDefaultAsync(g => g.Id == groupId);
            var expense = group?.Expenses.FirstOrDefault(e => e.Id == expenseId);

            if (expense == null) return false;

            group.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SettlementResult>?> CalculateSettlementAsync(Guid groupId)
        {
            var group = await _context.Groups
                .Include(g => g.Users)
                .Include(g => g.Expenses)
                .ThenInclude(e => e.SplitAmong)
                .Include(g => g.Expenses)
                .ThenInclude(e => e.PaidBy)
                .FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null) return null;

            var balances = group.Users.ToDictionary(u => u.Id, _ => 0m);

            foreach (var expense in group.Expenses)
            {
                var splitAmount = expense.Amount / expense.SplitAmong.Count;
                foreach (var user in expense.SplitAmong)
                    balances[user.Id] -= splitAmount;

                balances[expense.PaidBy.Id] += expense.Amount;
            }

            var creditors = new PriorityQueue<User, decimal>();
            var debtors = new PriorityQueue<User, decimal>();

            foreach (var (userId, amount) in balances)
            {
                var user = group.Users.First(u => u.Id == userId);
                if (amount > 0) creditors.Enqueue(user, -amount); // Negative priority for max credit first
                else if (amount < 0) debtors.Enqueue(user, amount); // Positive priority for max debt first
            }

            var settlements = new List<SettlementResult>();

            while (creditors.Count > 0 && debtors.Count > 0)
            {
                var creditor = creditors.Dequeue();
                var debtor = debtors.Dequeue();

                var credit = balances[creditor.Id];
                var debit = balances[debtor.Id];
                var amountToSettle = Math.Min(credit, -debit);

                settlements.Add(new SettlementResult
                {
                    FromUser = debtor,
                    ToUser = creditor,
                    Amount = amountToSettle
                });

                balances[creditor.Id] -= amountToSettle;
                balances[debtor.Id] += amountToSettle;

                if (balances[creditor.Id] > 0) creditors.Enqueue(creditor, -balances[creditor.Id]);
                if (balances[debtor.Id] < 0) debtors.Enqueue(debtor, balances[debtor.Id]);
            }

            return settlements;
        }
    }
}

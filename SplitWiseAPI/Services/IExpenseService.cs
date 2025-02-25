using SplitWiseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitWiseAPI.Services
{
    public interface IExpenseService
    {
        Task<Expense> AddExpenseAsync(Guid groupId, Expense expense);
        Task<bool> UpdateExpenseAsync(Guid groupId, Expense expense);
        Task<bool> DeleteExpenseAsync(Guid groupId, Guid expenseId);
        Task<List<SettlementResult>> CalculateSettlementAsync(Guid groupId);
    }
}
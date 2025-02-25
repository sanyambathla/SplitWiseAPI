using SplitWiseAPI.DTOs;
using SplitWiseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitWiseAPI.Services
{
    public interface IExpenseService
    {
        Task<ExpenseResponseDTO?> AddExpenseAsync(Guid groupId, ExpenseCreateDTO expenseDto);
        Task<bool> UpdateExpenseAsync(Guid groupId, ExpenseUpdateDTO expenseDto);
        Task<bool> DeleteExpenseAsync(Guid groupId, Guid expenseId);
        Task<List<SettlementResult>> CalculateSettlementAsync(Guid groupId);
    }
}
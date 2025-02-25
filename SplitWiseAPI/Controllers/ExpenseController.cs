using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.DTOs;
using SplitWiseAPI.Services;

namespace SplitWiseAPI.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) => _expenseService = expenseService;

        // ✅ Add Expense using ExpenseDTO
        [HttpPost("{groupId}/add")]
        public async Task<IActionResult> AddExpense(Guid groupId, ExpenseCreateDTO expenseDto)
        {
            var addedExpense = await _expenseService.AddExpenseAsync(groupId, expenseDto);
            return addedExpense != null
                ? CreatedAtAction(nameof(AddExpense), new { groupId, expenseId = addedExpense.Id }, addedExpense)
                : NotFound("Group not found.");
        }

        // 🔄 Update Expense using ExpenseUpdateDTO
        [HttpPut("{groupId}/update")]
        public async Task<IActionResult> UpdateExpense(Guid groupId, ExpenseUpdateDTO updateDto)
        {
            var updated = await _expenseService.UpdateExpenseAsync(groupId, updateDto);
            return updated ? Ok("Expense updated.") : NotFound("Expense or group not found.");
        }

        // ❌ Delete Expense
        [HttpDelete("{groupId}/delete/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(Guid groupId, Guid expenseId)
        {
            var deleted = await _expenseService.DeleteExpenseAsync(groupId, expenseId);
            return deleted ? Ok("Expense deleted.") : NotFound("Expense not found.");
        }

        // 📊 Calculate Settlement with SettlementResponseDTO
        [HttpGet("{groupId}/settlements")]
        public async Task<IActionResult> CalculateSettlement(Guid groupId)
        {
            var settlements = await _expenseService.CalculateSettlementAsync(groupId);
            return settlements != null && settlements.Any()
                ? Ok(settlements.Select(s => new SettlementResponseDTO(s)))
                : NotFound("Group or expenses not found.");
        }
    }
}

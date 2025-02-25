using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services;

namespace SplitWiseAPI.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService) => _expenseService = expenseService;

        [HttpPost("{groupId}/add")]
        public async Task<IActionResult> AddExpense(Guid groupId, Expense expense)
        {
            var addedExpense = await _expenseService.AddExpenseAsync(groupId, expense);
            return addedExpense != null
                ? CreatedAtAction(nameof(AddExpense), new { groupId, expenseId = addedExpense.Id }, addedExpense)
                : NotFound("Group not found.");
        }

        [HttpPut("{groupId}/update")]
        public async Task<IActionResult> UpdateExpense(Guid groupId, Expense expense)
        {
            var updated = await _expenseService.UpdateExpenseAsync(groupId, expense);
            return updated ? Ok("Expense updated.") : NotFound("Expense or group not found.");
        }

        [HttpDelete("{groupId}/delete/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(Guid groupId, Guid expenseId)
        {
            var deleted = await _expenseService.DeleteExpenseAsync(groupId, expenseId);
            return deleted ? Ok("Expense deleted.") : NotFound("Expense not found.");
        }

        [HttpGet("{groupId}/settlements")]
        public async Task<IActionResult> CalculateSettlement(Guid groupId)
        {
            var settlements = await _expenseService.CalculateSettlementAsync(groupId);
            return settlements != null && settlements.Any() ? Ok(settlements) : NotFound("Group or expenses not found.");
        }
    }
}

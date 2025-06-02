using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new UnauthorizedAccessException("Không thể xác định người dùng.");

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto budgetDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
            try
            {
                var createdBudget = await _budgetService.CreateBudgetAsync(budgetDto, userId);
                if (createdBudget == null) return BadRequest("Không thể tạo ngân sách.");
                return CreatedAtAction(nameof(GetBudgetById), new { budgetId = createdBudget.Id }, createdBudget);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBudgets()
        {
            var userId = GetUserId();
            var budgets = await _budgetService.GetBudgetsByUserIdAsync(userId);
            return Ok(budgets);
        }

        [HttpGet("{budgetId}")]
        public async Task<IActionResult> GetBudgetById(int budgetId)
        {
            var userId = GetUserId();
            var budget = await _budgetService.GetBudgetByIdAsync(budgetId, userId);
            if (budget == null) return NotFound("Không tìm thấy ngân sách.");
            return Ok(budget);
        }

        [HttpPut("{budgetId}")]
        public async Task<IActionResult> UpdateBudget(int budgetId, [FromBody] UpdateBudgetDto budgetDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
             try
            {
                var success = await _budgetService.UpdateBudgetAsync(budgetId, budgetDto, userId);
                if (!success) return NotFound("Không tìm thấy ngân sách hoặc bạn không có quyền cập nhật.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{budgetId}")]
        public async Task<IActionResult> DeleteBudget(int budgetId)
        {
            var userId = GetUserId();
            var success = await _budgetService.DeleteBudgetAsync(budgetId, userId);
            if (!success) return NotFound("Không tìm thấy ngân sách hoặc bạn không có quyền xóa.");
            return NoContent();
        }
    }
}
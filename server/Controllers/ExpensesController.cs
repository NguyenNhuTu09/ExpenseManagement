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
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new UnauthorizedAccessException("Không thể xác định người dùng.");

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto expenseDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
            try
            {
                var createdExpense = await _expenseService.CreateExpenseAsync(expenseDto, userId);
                if (createdExpense == null) return BadRequest("Không thể tạo chi tiêu.");
                return CreatedAtAction(nameof(GetExpenseById), new { expenseId = createdExpense.Id }, createdExpense);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyExpenses(
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate, 
            [FromQuery] int? categoryId,
            [FromQuery] string? sortBy = "date", // Mặc định sắp xếp theo ngày
            [FromQuery] bool ascending = false)  // Mặc định giảm dần
        {
            var userId = GetUserId();
            var expenses = await _expenseService.GetExpensesByUserIdAsync(userId, startDate, endDate, categoryId, sortBy, ascending);
            return Ok(expenses);
        }
        
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalExpenses(
            [FromQuery] DateTime startDate, 
            [FromQuery] DateTime endDate, 
            [FromQuery] int? categoryId)
        {
            var userId = GetUserId();
            if (startDate == default || endDate == default || endDate < startDate)
            {
                return BadRequest("Ngày bắt đầu và kết thúc không hợp lệ.");
            }
            var total = await _expenseService.GetTotalExpensesByUserIdAsync(userId, startDate, endDate, categoryId);
            return Ok(new { TotalAmount = total });
        }


        [HttpGet("{expenseId}")]
        public async Task<IActionResult> GetExpenseById(int expenseId)
        {
            var userId = GetUserId();
            var expense = await _expenseService.GetExpenseByIdAsync(expenseId, userId);
            if (expense == null) return NotFound("Không tìm thấy chi tiêu.");
            return Ok(expense);
        }

        [HttpPut("{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] UpdateExpenseDto expenseDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
            try
            {
                var success = await _expenseService.UpdateExpenseAsync(expenseId, expenseDto, userId);
                if (!success) return NotFound("Không tìm thấy chi tiêu hoặc bạn không có quyền cập nhật.");
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                 return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var userId = GetUserId();
            var success = await _expenseService.DeleteExpenseAsync(expenseId, userId);
            if (!success) return NotFound("Không tìm thấy chi tiêu hoặc bạn không có quyền xóa.");
            return NoContent();
        }
    }
}
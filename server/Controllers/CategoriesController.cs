using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Controllers
{
    [Authorize] // Yêu cầu xác thực cho tất cả các API trong controller này
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? throw new UnauthorizedAccessException("Không thể xác định người dùng.");

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto, userId);
            if (createdCategory == null) return BadRequest("Không thể tạo danh mục.");
            return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id }, createdCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCategories()
        {
            var userId = GetUserId();
            var categories = await _categoryService.GetCategoriesByUserIdAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var userId = GetUserId();
            var category = await _categoryService.GetCategoryByIdAsync(categoryId, userId);
            if (category == null) return NotFound("Không tìm thấy danh mục.");
            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = GetUserId();
            var success = await _categoryService.UpdateCategoryAsync(categoryId, categoryDto, userId);
            if (!success) return NotFound("Không tìm thấy danh mục hoặc bạn không có quyền cập nhật.");
            return NoContent();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var userId = GetUserId();
            try
            {
                var success = await _categoryService.DeleteCategoryAsync(categoryId, userId);
                if (!success) return NotFound("Không tìm thấy danh mục hoặc bạn không có quyền xóa.");
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message }); // Trả về 409 Conflict nếu danh mục đang được sử dụng
            }
        }
    }
}
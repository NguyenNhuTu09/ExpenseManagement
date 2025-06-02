using ExpenseManagement.Server.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto categoryDto, string userId);
        Task<IEnumerable<CategoryDto>> GetCategoriesByUserIdAsync(string userId);
        Task<CategoryDto?> GetCategoryByIdAsync(int categoryId, string userId);
        Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto categoryDto, string userId);
        Task<bool> DeleteCategoryAsync(int categoryId, string userId);
    }
}
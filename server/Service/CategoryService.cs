using AutoMapper;
using ExpenseManagement.Server.Data;
using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Entities;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto categoryDto, string userId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UserId = userId;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesByUserIdAsync(string userId)
        {
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int categoryId, string userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto categoryDto, string userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            if (category == null) return false;

            _mapper.Map(categoryDto, category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId, string userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            if (category == null) return false;

            var isUsedInExpense = await _context.Expenses.AnyAsync(e => e.CategoryId == categoryId && e.UserId == userId);
            if (isUsedInExpense)
            {
                throw new InvalidOperationException("Danh mục đang được sử dụng, không thể xóa.");
            }
             var isUsedInBudget = await _context.Budgets.AnyAsync(b => b.CategoryId == categoryId && b.UserId == userId);
            if (isUsedInBudget)
            {
                throw new InvalidOperationException("Danh mục đang được sử dụng trong ngân sách, không thể xóa.");
            }


            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using AutoMapper;
using ExpenseManagement.Server.Data;
using ExpenseManagement.Server.Dtos;
using ExpenseManagement.Server.Entities;
using ExpenseManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExpenseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto expenseDto, string userId)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == expenseDto.CategoryId && c.UserId == userId);
            if (!categoryExists)
            {
                throw new ArgumentException("Danh mục không hợp lệ hoặc không thuộc về người dùng này.");
            }

            var expense = _mapper.Map<Expense>(expenseDto);
            expense.UserId = userId;
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            
            await _context.Entry(expense).Reference(e => e.Category).LoadAsync();
            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(
            string userId, DateTime? startDate, DateTime? endDate, int? categoryId, string? sortBy, bool ascending = true)
        {
            var query = _context.Expenses
                .Where(e => e.UserId == userId)
                .Include(e => e.Category) // Eager loading Category
                .AsQueryable();

            if (startDate.HasValue)
                query = query.Where(e => e.Date >= startDate.Value.Date); // So sánh chỉ ngày
            if (endDate.HasValue)
                query = query.Where(e => e.Date <= endDate.Value.Date.AddDays(1).AddTicks(-1)); // Đến cuối ngày endDate
            if (categoryId.HasValue && categoryId > 0)
                query = query.Where(e => e.CategoryId == categoryId.Value);

            // Sắp xếp
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                switch (sortBy.ToLowerInvariant())
                {
                    case "date":
                        query = ascending ? query.OrderBy(e => e.Date) : query.OrderByDescending(e => e.Date);
                        break;
                    case "amount":
                        query = ascending ? query.OrderBy(e => e.Amount) : query.OrderByDescending(e => e.Amount);
                        break;
                    case "category":
                        query = ascending ? query.OrderBy(e => e.Category!.Name) : query.OrderByDescending(e => e.Category!.Name);
                        break;
                    default: // Mặc định sắp xếp theo ngày giảm dần
                        query = query.OrderByDescending(e => e.Date);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(e => e.Date); // Mặc định
            }

            var expenses = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        }
        
        public async Task<decimal> GetTotalExpensesByUserIdAsync(string userId, DateTime startDate, DateTime endDate, int? categoryId)
        {
            var query = _context.Expenses
                .Where(e => e.UserId == userId && e.Date >= startDate.Date && e.Date <= endDate.Date.AddDays(1).AddTicks(-1));

            if (categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            return await query.SumAsync(e => e.Amount);
        }


        public async Task<ExpenseDto?> GetExpenseByIdAsync(int expenseId, string userId)
        {
            var expense = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);
            return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<bool> UpdateExpenseAsync(int expenseId, UpdateExpenseDto expenseDto, string userId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null) return false;

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == expenseDto.CategoryId && c.UserId == userId);
            if (!categoryExists)
            {
                 throw new ArgumentException("Danh mục không hợp lệ hoặc không thuộc về người dùng này.");
            }

            _mapper.Map(expenseDto, expense);
            expense.UpdatedAt = DateTime.UtcNow;
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId, string userId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == userId);

            if (expense == null) return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
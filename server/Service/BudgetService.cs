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
    public class BudgetService : IBudgetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExpenseService _expenseService; // Để lấy tổng chi tiêu

        public BudgetService(ApplicationDbContext context, IMapper mapper, IExpenseService expenseService)
        {
            _context = context;
            _mapper = mapper;
            _expenseService = expenseService;
        }

        public async Task<BudgetDto?> CreateBudgetAsync(CreateBudgetDto budgetDto, string userId)
        {
            if (budgetDto.EndDate < budgetDto.StartDate)
            {
                throw new ArgumentException("Ngày kết thúc không thể trước ngày bắt đầu.");
            }
            if (budgetDto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == budgetDto.CategoryId.Value && c.UserId == userId);
                if (!categoryExists)
                {
                    throw new ArgumentException("Danh mục không hợp lệ hoặc không thuộc về người dùng này.");
                }
            }

            var budget = _mapper.Map<Budget>(budgetDto);
            budget.UserId = userId;
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            
            var budgetResult = _mapper.Map<BudgetDto>(budget);
            budgetResult.SpentAmount = await _expenseService.GetTotalExpensesByUserIdAsync(userId, budget.StartDate, budget.EndDate, budget.CategoryId);
            return budgetResult;
        }

        public async Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdAsync(string userId)
        {
            var budgets = await _context.Budgets
                .Where(b => b.UserId == userId)
                .Include(b => b.Category)
                .OrderByDescending(b => b.EndDate)
                .ToListAsync();
            
            var budgetDtos = _mapper.Map<List<BudgetDto>>(budgets);
            foreach(var dto in budgetDtos)
            {
                var budgetEntity = budgets.First(b => b.Id == dto.Id);
                dto.SpentAmount = await _expenseService.GetTotalExpensesByUserIdAsync(userId, budgetEntity.StartDate, budgetEntity.EndDate, budgetEntity.CategoryId);
            }
            return budgetDtos;
        }

        public async Task<BudgetDto?> GetBudgetByIdAsync(int budgetId, string userId)
        {
            var budget = await _context.Budgets
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == budgetId && b.UserId == userId);
            
            if (budget == null) return null;

            var budgetDto = _mapper.Map<BudgetDto>(budget);
            budgetDto.SpentAmount = await _expenseService.GetTotalExpensesByUserIdAsync(userId, budget.StartDate, budget.EndDate, budget.CategoryId);
            return budgetDto;
        }

        public async Task<bool> UpdateBudgetAsync(int budgetId, UpdateBudgetDto budgetDto, string userId)
        {
            var budget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == budgetId && b.UserId == userId);

            if (budget == null) return false;
            
            if (budgetDto.EndDate < budgetDto.StartDate)
            {
                throw new ArgumentException("Ngày kết thúc không thể trước ngày bắt đầu.");
            }
            if (budgetDto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories.AnyAsync(c => c.Id == budgetDto.CategoryId.Value && c.UserId == userId);
                if (!categoryExists)
                {
                    throw new ArgumentException("Danh mục không hợp lệ hoặc không thuộc về người dùng này.");
                }
            }

            _mapper.Map(budgetDto, budget);
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId, string userId)
        {
            var budget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == budgetId && b.UserId == userId);

            if (budget == null) return false;

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
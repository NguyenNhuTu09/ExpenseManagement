using ExpenseManagement.Server.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ExpenseDto?> CreateExpenseAsync(CreateExpenseDto expenseDto, string userId);
        Task<IEnumerable<ExpenseDto>> GetExpensesByUserIdAsync(string userId, DateTime? startDate, DateTime? endDate, int? categoryId, string? sortBy, bool ascending = true);
        Task<ExpenseDto?> GetExpenseByIdAsync(int expenseId, string userId);
        Task<bool> UpdateExpenseAsync(int expenseId, UpdateExpenseDto expenseDto, string userId);
        Task<bool> DeleteExpenseAsync(int expenseId, string userId);
        Task<decimal> GetTotalExpensesByUserIdAsync(string userId, DateTime startDate, DateTime endDate, int? categoryId);
    }
}
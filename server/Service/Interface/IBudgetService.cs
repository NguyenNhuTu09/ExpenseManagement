using ExpenseManagement.Server.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.Server.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetDto?> CreateBudgetAsync(CreateBudgetDto budgetDto, string userId);
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdAsync(string userId);
        Task<BudgetDto?> GetBudgetByIdAsync(int budgetId, string userId);
        Task<bool> UpdateBudgetAsync(int budgetId, UpdateBudgetDto budgetDto, string userId);
        Task<bool> DeleteBudgetAsync(int budgetId, string userId);
    }
}

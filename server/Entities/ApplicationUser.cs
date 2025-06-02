using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ExpenseManagement.Server.Entities
{
    
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>(); // User có thể có danh mục riêng
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}

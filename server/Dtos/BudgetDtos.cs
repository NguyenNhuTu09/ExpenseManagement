namespace ExpenseManagement.Server.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BudgetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; } // Tên danh mục (nếu có)
        public string UserId { get; set; } = string.Empty;
        public decimal SpentAmount { get; set; } // Số tiền đã chi tiêu trong ngân sách này
        public decimal RemainingAmount => Amount - SpentAmount;
        public double Progress => Amount > 0 ? (double)SpentAmount / (double)Amount : 0;
    }

    public class CreateBudgetDto
    {
        [Required(ErrorMessage = "Tên ngân sách là bắt buộc.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số tiền ngân sách là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        public DateTime EndDate { get; set; }

        public int? CategoryId { get; set; } // Nullable nếu là ngân sách tổng thể cho người dùng
    }

    public class UpdateBudgetDto : CreateBudgetDto
    {
    }
}
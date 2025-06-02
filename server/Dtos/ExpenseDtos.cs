namespace ExpenseManagement.Server.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Để hiển thị tên danh mục
        public string? CategoryIcon { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateExpenseDto
    {
        [Required(ErrorMessage = "Mô tả chi tiêu là bắt buộc.")]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số tiền là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Ngày chi tiêu là bắt buộc.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        public int CategoryId { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdateExpenseDto : CreateExpenseDto
    {
    }
}
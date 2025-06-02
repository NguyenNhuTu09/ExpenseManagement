using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManagement.Server.Entities
{
    public class Budget
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền ngân sách phải lớn hơn 0.")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Nếu ngân sách này cho một danh mục cụ thể
        public int? CategoryId { get; set; } // Nullable nếu là ngân sách tổng thể
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Tên ngân sách, ví dụ "Ngân sách ăn uống tháng 6"
    }
}
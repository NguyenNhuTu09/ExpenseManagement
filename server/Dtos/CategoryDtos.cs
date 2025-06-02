namespace ExpenseManagement.Server.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string UserId { get; set; } = string.Empty;
    }

    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Icon { get; set; }
    }

    public class UpdateCategoryDto : CreateCategoryDto
    {
    }
}
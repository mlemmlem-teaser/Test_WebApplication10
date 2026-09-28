using System.ComponentModel.DataAnnotations;

namespace Test_WebApplication10.Models
{
    public class Product : IValidatableObject
    {
        [Required(ErrorMessage = "Id là bắt buộc nhập")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc nhập")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ảnh là bắt buộc chọn")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Giá chuẩn là bắt buộc nhập")]
        [Range(100000, double.MaxValue, ErrorMessage = "Giá chuẩn phải có giá trị nhỏ nhất là 100,000")]
        [DataType(DataType.Text)]
        public float Price { get; set; }

        [Required(ErrorMessage = "Giá khuyến mãi là bắt buộc nhập")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi không được âm")]
        public float SalePrice { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc nhập")]
        [MaxLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ID Danh mục là bắt buộc nhập")]
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SalePrice > Price * 0.9)
            {
                yield return new ValidationResult("Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10%.", new[] { nameof(SalePrice) });
            }

            if (!string.IsNullOrEmpty(Description))
            {
                string[] sensitiveWords = { "die", "admin", "fack" }; 
                foreach (var word in sensitiveWords)
                {
                    if (Description.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        yield return new ValidationResult($"Mô tả không được chứa từ nhạy cảm: '{word}'.", new[] { nameof(Description) });
                    }
                }
            }
        }
    }
}

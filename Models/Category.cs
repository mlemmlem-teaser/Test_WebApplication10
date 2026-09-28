using System.ComponentModel.DataAnnotations;

namespace Test_WebApplication10.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Id là bắt buộc nhập")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc nhập")]
        public string Name { get; set; }
    }
}

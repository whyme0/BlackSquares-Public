using System.ComponentModel.DataAnnotations;

namespace BlackSquares.Models
{
    public class CreateMemeModel
    {
        [Display(Name = "Основной текст")]
        [MaxLength(23, ErrorMessage = "Максимальная длина основного текста - 23 символов.")]
        public string? MainText { get; set; }

        [Display(Name = "Дополнительный текст")]
        [MaxLength(33, ErrorMessage = "Максимальная длина дополнительного текста - 33 символов.")]
        public string? ExtraText { get; set; }

        [Required(ErrorMessage = "Обязательно поле."), Display(Name = "Изображение")]
        public IFormFile Image { get; set; }
    }
}

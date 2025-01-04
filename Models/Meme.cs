using System.ComponentModel.DataAnnotations;

namespace BlackSquares.Models
{
    public class Meme
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public Meme()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}

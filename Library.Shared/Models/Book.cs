using System.ComponentModel.DataAnnotations;
using Library.Shared.Validations;

namespace Library.Shared.Models
{
	public class Book
	{
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required]
        [MaxLength(100)]
        public string ISBN { get; set; }

        [Required]
        [NoFutureDate]
        public DateTime PublishedDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }
    }
}
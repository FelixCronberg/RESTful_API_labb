using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Labb2.Models
{
	public class Book
	{
		public int BookId { get; set; }

		public required string Title { get; set; }
		public string? ISBN { get; set; }
		public decimal? Rating { get; set; }
		public int? ReleaseYear { get; set; }
		public bool IsAvailable { get; set; } = true;

		//Nav
		public ICollection<Author> Authors { get; set; } = [];
		public ICollection<Loan> TimesLoaned { get; set; } = [];

	}
}

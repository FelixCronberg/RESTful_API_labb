using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class Book
	{
		public int BookId { get; set; }

		[Required]
		public required string Title { get; set; }
		public string? ISBN { get; set; }
		public int? Rating { get; set; }
		public int? ReleaseYear { get; set; }
		public bool Available { get; set; } = true;

		//Nav prop
		[Required]
		public ICollection<Author> Authors { get; set; } = [];


	}
}

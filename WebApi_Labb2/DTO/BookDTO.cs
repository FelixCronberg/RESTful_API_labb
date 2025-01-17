using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class BookDTO
	{
		public required string Title { get; set; }
		public string? ISBN { get; set; }
		public int? Rating { get; set; }
		public int? ReleaseYear { get; set; }
		public bool Available { get; set; } = true;

		public ICollection<Author> Authors { get; set; } = [];
	}
}

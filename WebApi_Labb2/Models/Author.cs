using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class Author
	{
		public int AuthorId { get; set; }

		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		//Nav
		public ICollection<Book> Books { get; set; } = [];

	}
}

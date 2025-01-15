using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class Author
	{
		public int AuthorId { get; set; }

		[Required]
		public required string FirstName { get; set; }
		[Required]
		public required string LastName { get; set; }

		//Nav
		[Required]
		public ICollection<Book> Books { get; set; } = [];


	}
}

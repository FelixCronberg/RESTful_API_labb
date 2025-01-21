using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class AuthorDTO
	{
		public int AuthorId { get; set; }

		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		public List<ShortBookDTO> Books { get; set; } = [];
	}
}

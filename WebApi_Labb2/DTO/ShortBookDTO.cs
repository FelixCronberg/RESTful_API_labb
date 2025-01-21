namespace WebApi_Labb2.DTO
{
	public class ShortBookDTO
	{
		public int BookID { get; set; }
		public required string Title { get; set; }
		public int? ReleaseYear { get; set; }
	}
}

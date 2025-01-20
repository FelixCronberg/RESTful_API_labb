namespace WebApi_Labb2.Controllers.Request_Bodies
{
	public class AssignAuthorsToBook
	{
		public int BookId { get; set; }
		public List<int> AuthorIds { get; set; } = [];
	}
}

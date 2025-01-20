namespace WebApi_Labb2.Controllers.Request_Bodies
{
	public class AssignBooksToAuthor
	{
		public int AuthorId { get; set; }
		public List<int> BookIds { get; set; } = [];
	}
}

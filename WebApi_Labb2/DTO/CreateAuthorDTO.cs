using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateAuthorDTO
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
	}
}

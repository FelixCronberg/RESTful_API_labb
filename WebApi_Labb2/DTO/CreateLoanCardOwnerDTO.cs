using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.DTO
{
	public class CreateLoanCardOwnerDTO
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
	}
}

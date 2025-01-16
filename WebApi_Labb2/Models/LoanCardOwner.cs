using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class LoanCardOwner
	{
		public int LoanCardOwnerId { get; set; }

		[Required]
		public required string FirstName { get; set; }
		[Required]
		public required string LastName { get; set; }



	}
}

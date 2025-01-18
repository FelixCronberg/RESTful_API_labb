using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class LoanCardOwner
	{
		public int LoanCardOwnerId { get; set; }

		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		//Nav
		public required LoanCard LoanCard { get; set; }

	}
}

using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class LoanCard
	{
		public int LoanCardId { get; set; }

		[Required]
		public DateOnly IssueDate { get; set; }
		[Required]
		public DateOnly ExpirationDate { get; set; }

		//FK
		public int LoanCardOwnerId { get; set; }

		//Nav
		[Required]
		public required LoanCardOwner LoanCardOwner { get; set; }
		//Returned loans should be removed from here, but the reference to the card should stay in the loan
		//Check if this works later
		public ICollection<Loan> ActiveLoans { get; set; } = [];

	}
}

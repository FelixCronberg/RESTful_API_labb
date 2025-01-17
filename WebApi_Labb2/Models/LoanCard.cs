using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class LoanCard
	{
		public int LoanCardId { get; set; }

		[Required]
		public DateTime IssueDate { get; set; }
		[Required]
		public DateTime ExpirationDate { get; set; }

		//Nav prop
		[Required]
		public required LoanCardOwner LoanCardOwner { get; set; }

		//Returned loans should be removed from here, but the reference to the card should stay in the loan
		//Check if this works later
		public ICollection<Loan> ActiveLoans { get; set; } = [];


	}
}

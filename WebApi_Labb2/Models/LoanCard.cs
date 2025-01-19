using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi_Labb2.Models
{
	public class LoanCard
	{
		public int LoanCardId { get; set; }

		public DateOnly IssueDate { get; set; }
		public DateOnly ExpirationDate { get; set; }

		//FK
		public int LoanCardOwnerId { get; set; }

		//Nav
		[JsonIgnore]
		public LoanCardOwner LoanCardOwner { get; set; } = null!;
		//Returned loans should be removed from here, but the reference to the card should stay in the loan
		//Check if this works later
		public ICollection<Loan> ActiveLoans { get; set; } = [];

	}
}

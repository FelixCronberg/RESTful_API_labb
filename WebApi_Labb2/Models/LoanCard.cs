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
		public ICollection<Loan> Loans { get; set; } = [];

	}
}

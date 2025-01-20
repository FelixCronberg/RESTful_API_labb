using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi_Labb2.Models
{
	public class Loan
	{
		public int LoanId { get; set; }

		public DateTime LoanedDate { get; set; }
		public DateTime? ReturnedDate { get; set; }

		//FK
		public int BookId { get; set; }
		public int LoanCardId { get; set; }

		//Nav
		[JsonIgnore]
		public Book Book { get; set; } = null!;

		[JsonIgnore]
		//Reference to loanCard should stay here but be removed from LoanCard ActiveLoans 
		public LoanCard LoanCard { get; set; } = null!;

	}
}

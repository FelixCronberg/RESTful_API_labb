using System.ComponentModel.DataAnnotations;

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
		public required Book Book { get; set; }
		//Reference to loanCard should stay here but be removed from LoanCard ActiveLoans 
		public required LoanCard LoanCard { get; set; }

	}
}

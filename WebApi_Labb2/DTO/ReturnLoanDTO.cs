using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class ReturnLoanDTO
	{
		public int LoanId { get; set; }

		//Unsure how this would work
		//Need BookId and LoanCardId to make book available again and remove loan from LoanCard ActiveLoans

		//But if you have LoanId you do have both so maybe don't need anything else
		//Sounds right so commenting this away for now
		//public required Book Book { get; set; }
		//public required LoanCard LoanCard { get; set; }
	}
}

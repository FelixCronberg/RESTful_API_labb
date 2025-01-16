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
		public LoanCardOwner LoanCardOwner { get; set; } = null!;
		public ICollection<Loan> Loans { get; set; } = [];


	}
}

using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateLoanCardDTO
	{
		public DateTime IssueDate { get; set; }

		public required LoanCardOwner LoanCardOwner { get; set; }
	}
}

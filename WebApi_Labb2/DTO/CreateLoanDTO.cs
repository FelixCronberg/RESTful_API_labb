using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateLoanDTO
	{
		public required int BookId { get; set; }
		public required int LoanCardId { get; set; }
	}
}

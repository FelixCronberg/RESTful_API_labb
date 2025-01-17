using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateLoanDTO
	{
		public required Book Book { get; set; }
		public required LoanCard LoanCard { get; set; }
	}
}

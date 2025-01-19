using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateLoanCardDTO
	{
		public required int LoanCardOwnerId { get; set; }
	}
}

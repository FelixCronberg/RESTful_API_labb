﻿using System.ComponentModel.DataAnnotations;

namespace WebApi_Labb2.Models
{
	public class Loan
	{
		public int LoanId { get; set; }

		[Required]
		public DateOnly LoanedDate { get; set; }
		public DateOnly? ReturnDate { get; set; }

		//Nav prop
		public required Book Book { get; set; }
		//Reference to loanCard should stay here but be removed from LoanCard ActiveLoans 
		public required LoanCard LoanCard { get; set; }


	}
}

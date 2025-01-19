using WebApi_Labb2.DTO;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.Extensions
{
	public static class DTOExtensions
	{
		public static Author ToAuthor(this CreateAuthorDTO authorDTO)
		{
			return new Author
			{
				FirstName = authorDTO.FirstName,
				LastName = authorDTO.LastName
			};
		}





		public static LoanCard ToLoanCard(this CreateLoanCardDTO loanCardDTO)
		{
			return new LoanCard
			{
				LoanCardOwnerId = loanCardDTO.LoanCardOwnerId,
			};
		}
		public static LoanCardOwner ToLoanCardOwner(this CreateLoanCardOwnerDTO loanCardOwnerDTO)
		{
			return new LoanCardOwner
			{
				FirstName = loanCardOwnerDTO.FirstName,
				LastName = loanCardOwnerDTO.LastName
			};
		}
	}
}

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
		public static Book ToBook(this CreateBookDTO bookDTO)
		{
			return new Book
			{
				Title = bookDTO.Title,
				ISBN = bookDTO.ISBN,
				Rating = bookDTO.Rating,
				ReleaseYear = bookDTO.ReleaseYear
			};
		}
		public static BookDTO ToBookDTO(this Book book)
		{
			return new BookDTO
			{
				BookId = book.BookId,
				Title = book.Title,
				ISBN = book.ISBN,
				Rating = book.Rating,
				ReleaseYear = book.ReleaseYear,
				IsAvailable = book.IsAvailable,
				Authors = book.Authors
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

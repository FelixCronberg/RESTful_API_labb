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
		public static AuthorDTO ToAuthorDTO(this Author author)
		{
			return new AuthorDTO
			{
				AuthorId = author.AuthorId,
				FirstName = author.FirstName,
				LastName = author.LastName,
				Books = author.Books.Select(b => b.ToShortBookDTO()).ToList(),
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
		public static ShortBookDTO ToShortBookDTO(this Book book)
		{
			return new ShortBookDTO
			{
				Title = book.Title,
				BookID = book.BookId,
				ReleaseYear = book.ReleaseYear
			};
		}

		public static Loan ToLoan(this CreateLoanDTO loanDTO)
		{
			return new Loan
			{
				BookId = loanDTO.BookId,
				LoanCardId = loanDTO.LoanCardId,
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

using Microsoft.EntityFrameworkCore;

namespace WebApi_Labb2.Models
{
	public class BooksDbContext: DbContext
	{
		public BooksDbContext(DbContextOptions<BooksDbContext> contextOptions): base(contextOptions)
		{
		}

		DbSet<Book> Books { get; set; }
		DbSet<Author> Author { get; set; }
		DbSet<LoanCard> LoanCard { get; set; }
		DbSet<LoanCardOwner> LoanCardOwner { get; set; }
		DbSet<Loan> Loan { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Add Cascade delete for BookAuthor Junc table, and ? maybe also on LoanCardOwner???

			//Look into this, where is the modelBuilder coming from
			//base.OnModelCreating(modelBuilder);
		}
	}
}

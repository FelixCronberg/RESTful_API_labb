using Microsoft.EntityFrameworkCore;

namespace WebApi_Labb2.Models
{
	public class BooksDbContext: DbContext
	{
		public BooksDbContext(DbContextOptions<BooksDbContext> contextOptions): base(contextOptions)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Author { get; set; }
		public DbSet<LoanCard> LoanCard { get; set; }
		public DbSet<LoanCardOwner> LoanCardOwner { get; set; }
		public DbSet<Loan> Loan { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			base.OnModelCreating(modelBuilder);

			#region ForeignKey definitions
			
			//Making all FKs required for now see if need to change later
			modelBuilder.Entity<Author>()
				.HasMany(a => a.Books)
				.WithMany(b => b.Authors)
				.UsingEntity<Dictionary<string, object>>(
				"AuthorBook",
				j => j.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.Cascade).IsRequired(),
				j => j.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.Cascade).IsRequired()
				);

			modelBuilder.Entity<Loan>()
				.HasOne(l => l.LoanCard)
				.WithMany(lc => lc.Loans)
				.HasForeignKey(l => l.LoanCardId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			modelBuilder.Entity<Loan>()
				.HasOne(l => l.Book)
				.WithMany(lc => lc.TimesLoaned)
				.HasForeignKey(l => l.BookId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			//Check migration to make sure FK LoanCardOwnerId has unique constraint
			modelBuilder.Entity<LoanCard>()
				.HasOne(lc => lc.LoanCardOwner)
				.WithOne(lco => lco.LoanCard)
				.HasForeignKey<LoanCard>(lc => lc.LoanCardOwnerId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			#endregion


			#region IsRequired

			modelBuilder.Entity<Book>()
				.Property(b => b.Title).HasMaxLength(120).IsRequired();

			modelBuilder.Entity<Author>()
				.Property(a => a.FirstName).HasMaxLength(70).IsRequired();
			modelBuilder.Entity<Author>()
				.Property(a => a.LastName).HasMaxLength(70).IsRequired();

			modelBuilder.Entity<LoanCardOwner>()
				.Property(a => a.FirstName).HasMaxLength(70).IsRequired();
			modelBuilder.Entity<LoanCardOwner>()
				.Property(a => a.LastName).HasMaxLength(70).IsRequired();


			//Example for applying same config for multiple properties
			//var customerProperties = new[] { "Name", "Email", "Phone" };

			//foreach (var propertyName in customerProperties)
			//{
			//	modelBuilder.Entity<Customer>()
			//		.Property(propertyName)
			//		.IsRequired()
			//		.HasMaxLength(100);
			//}

			#endregion


			#region DataTypes

			modelBuilder.Entity<Book>()
				.Property(b => b.Rating).HasColumnType("decimal(2,1)").IsRequired(false);

			modelBuilder.Entity<Loan>()
				.Property(l => l.LoanedDate).HasColumnType("smalldatetime");
			modelBuilder.Entity<Loan>()
				.Property(l => l.ReturnedDate).HasColumnType("smalldatetime");

			#endregion


			#region Check Constraints

			//Book rating + releaseYear.
			//Seems like you could make a constraint to check if a string matches ISBN pattern but I'm finding multiple ISBN Formats online so SKIP
			modelBuilder.Entity<Book>()
				.ToTable(t => t.HasCheckConstraint("CK_Book_Rating", "[Rating] > 0 AND [Rating] < 10"));
			modelBuilder.Entity<Book>()
				.ToTable(t => t.HasCheckConstraint("CK_Book_ReleaseYear", "[ReleaseYear] > 0 AND [ReleaseYear] < 2500"));

			#endregion


			#region Default values

			//Book IsAvailable + loaned date + issue/expiration date
			modelBuilder.Entity<Book>()
				.Property(b => b.IsAvailable).HasDefaultValue(true);

			modelBuilder.Entity<Loan>()
				.Property(l => l.LoanedDate).HasDefaultValueSql("GETDATE()").IsRequired();
			modelBuilder.Entity<Loan>()
				.Property(l => l.ReturnedDate).HasDefaultValue(null);

			modelBuilder.Entity<LoanCard>()
				.Property(l => l.IssueDate).HasDefaultValueSql("GETDATE()").IsRequired();
			modelBuilder.Entity<LoanCard>()
				.Property(l => l.ExpirationDate).HasDefaultValueSql("DATEADD(YEAR, 5, GETDATE())").IsRequired();

			#endregion

			
		}
	}
}

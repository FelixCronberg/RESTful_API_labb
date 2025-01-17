using Microsoft.EntityFrameworkCore;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddBooksDbContext(this IServiceCollection servColl, IConfiguration configuration)
		{
			servColl.AddDbContext<BooksDbContext>(opt => 
			opt.UseSqlServer(configuration.GetConnectionString("Development")));

			return servColl;
		}

	}
}

using Microsoft.EntityFrameworkCore;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureServicesInMemoryDatabase(this IServiceCollection servColl, IConfiguration configuration)
		{
			//Haven't merged yet so BooksDbContext doesn't work yet
			servColl.AddDbContext<BooksDbContext>(opt => 
			opt.UseSqlServer(configuration.GetConnectionString("Development")));

			return servColl;
		}

	}
}

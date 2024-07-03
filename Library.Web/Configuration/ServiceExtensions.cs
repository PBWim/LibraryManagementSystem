using Library.Repository.Contract;
using Library.Repository.Implementation;
using Library.Service.Contract;
using Library.Service.Implementation;

namespace Library.Web.Configuration
{
	public static class ServiceExtensions
	{
        public static void AddServices(this IServiceCollection services)
        {
            // Repos
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
        }
    }
}
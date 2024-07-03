using Library.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Configuration
{
    public class DatabaseHelper
    {
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<DatabaseHelper>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }

        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.Roles.Any())
            {
                // Database has been seeded
                return;
            }

            // Seed initial data
            SeedRoles(context);

            context.SaveChanges();
        }

        private static void SeedRoles(ApplicationDbContext context)
        {
            var roles = new[]
            {
                new IdentityRole("Admin"),
                new IdentityRole("User")
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }
        }
    }
}
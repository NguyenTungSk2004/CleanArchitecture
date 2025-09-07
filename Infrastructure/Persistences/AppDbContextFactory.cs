// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;

// namespace Infrastructure.Persistence
// {
//     public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//     {
//         public AppDbContext CreateDbContext(string[] args)
//         {
//             var configuration = new ConfigurationBuilder()
//                 .SetBasePath(Directory.GetCurrentDirectory()) // đảm bảo đúng path
//                 .AddJsonFile("appsettings.json")
//                 .Build();

//             var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//             var connectionString = configuration.GetConnectionString("DefaultConnection");

//             optionsBuilder.UseSqlite(connectionString);

//             return new AppDbContext(optionsBuilder.Options);
//         }
//     }
// }

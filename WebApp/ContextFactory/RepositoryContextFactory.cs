using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories.EFCore;

namespace WebApp.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            //configurationBuilder -->appsettings e ulaşmak için

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            //DbContextOptionBuilder--->appsettings den gelen connection string ile db context i buluşturuyor.

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                 p=>p.MigrationsAssembly("WebApp"));//Oluşturduğum migrationların WebApi projesinin altına eklenmesini istiyorum.

            return new RepositoryContext(builder.Options);
        }
    }
}

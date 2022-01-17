using FM.Application.Interfaces.Repositories;
using FM.Infrastructure.Contexts;
using FM.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FM.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }


        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            //services.AddTransient(typeof(IRepository<>), typeof(RepositoryAsync<>));
            //services.AddTransient<IProductRepository, ProductRepository>();
            //services.AddTransient<IProductCacheRepository, ProductCacheRepository>();
            //services.AddTransient<IBrandRepository, BrandRepository>();
            //services.AddTransient<IBrandCacheRepository, BrandCacheRepository>();
            //services.AddTransient<ILogRepository, LogRepository>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IHealthHistoryRepository, HealthHistoryRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            #endregion Repositories
        }
    }
}
using DapperCQRS.Commands.Handlers;
using DapperCQRS.Data;
using DapperCQRS.Queries.Handlers;

namespace DapperCQRS.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<DbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
            services.AddScoped<GetAllCategoriesHandler>();
            services.AddScoped<GetCategoryByIdHandler>();
            services.AddScoped<CreateCategoryHandler>();
            services.AddScoped<DeleteCategoryHandler>();
            services.AddScoped<UpdateCategoryHandler>();
        }
    }
}

using LibraryApp.DAL.Entities;
using LibraryApp.DAL.Persistence.Interfaces;
using LibraryApp.DAL.Persistence.Repositories;
using LibraryApp.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LibraryApp.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LibraryContext>(options =>
                        options.UseSqlServer(connectionString))
                               .AddLogging(configure => configure.SetMinimumLevel(LogLevel.None));

            services.AddScoped<IRepository<Book>, BookRepository>();
            services.AddScoped<IRepository<Form>, FormRepository>();
            services.AddScoped<IRepository<Reader>, ReaderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

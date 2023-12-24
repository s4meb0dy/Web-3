using LibraryApp.BLL.Interfaces;
using LibraryApp.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IReaderService, ReaderService>();

            return services;
        }
    }
}

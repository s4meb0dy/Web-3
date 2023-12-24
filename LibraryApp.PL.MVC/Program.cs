using LibraryApp.BLL;
using LibraryApp.DAL;

namespace LibraryApp.PL.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddBusinessLogicLayer();
            var connectionString = builder.Configuration.GetConnectionString("SqlServerConnectionString");
            builder.Services.AddDataAccessLayer(connectionString);


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=GetBooks}/{id?}");

            app.Run();
        }
    }
}
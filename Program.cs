using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Test_WebApplication10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Test_WebApplication10Context") ?? throw new InvalidOperationException("Connection string 'Test_WebApplication10Context' not found.");

            builder.Services.AddDbContext<Test_WebApplication10Context>(options =>
                options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            // Singular aliases so /Category and /Product work
            app.MapControllerRoute(
                name: "category_singular",
                pattern: "Category/{action=Index}/{id?}",
                defaults: new { controller = "Categories" })
                .WithStaticAssets();

            app.MapControllerRoute(
                name: "product_singular",
                pattern: "Product/{action=Index}/{id?}",
                defaults: new { controller = "Products" })
                .WithStaticAssets();

            // Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();


            // Ensure database is created/migrated on startup (applies to both Category and Product DbSets)
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Test_WebApplication10Context>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}

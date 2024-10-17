using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Repositories;
using LibraryManagmentSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace LibraryManagmentSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Library2"));
            });

            builder.Services.AddIdentity<User, IdentityRole>(
                option =>
                {
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                }
                ).AddEntityFrameworkStores<LibraryContext>();

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBooksCheckedOutRepository, BooksCheckedOutRepository>();
            builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
            builder.Services.AddScoped<IBooksReturnedRepository, BooksReturnedRepository>();
            builder.Services.AddScoped<ICheckOutRepository, CheckOutRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            builder.Services.AddScoped<IUsersBooks, UsersBooksRepository>();

            // Add dependency injection for User and IdentityRole
            builder.Services.AddScoped<User>();
            builder.Services.AddScoped<IdentityRole>(); 
            builder.Services.AddScoped<RoleInitializer>();


            // Add the configuration service
            builder.Services.AddSingleton(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Book}/{action=Index}/{id?}");



            
            // Initialize roles and users
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var roleInitializer = serviceProvider.GetRequiredService<RoleInitializer>();
                await roleInitializer.InitializeAsync();
            }


            app.Run();
        }
    }
}

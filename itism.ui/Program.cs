using itism.bll.Services;
using itism.bll.Services.Interface;
using itism.dal.Models.Data;
using itism.dal.Repositories;
using itism.dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace itism.ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IGradeService, GradeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.Configure<Configs.PageSettings>(builder.Configuration.GetSection("PageSettings"));


            var app = builder.Build();

            // Ensure database exists at startup (development only). Replace with migrations when CLI is available.
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                try
                {
                    var created = dbContext.Database.EnsureCreated();
                    Console.WriteLine($"Database created: {created}");
                    
                    // Test database connection
                    var canConnect = dbContext.Database.CanConnect();
                    Console.WriteLine($"Can connect to database: {canConnect}");
                    
                    // Add sample data if database is empty
                    if (!dbContext.Users.Any())
                    {
                        var sampleInstructor = new itism.dal.Models.User
                        {
                            Name = "Ahmed Instructor",
                            Email = "ahmed@example.com",
                            Role = itism.dal.Models.UserRole.Instructor,
                            IsActive = true
                        };
                        dbContext.Users.Add(sampleInstructor);
                        dbContext.SaveChanges();
                        Console.WriteLine("Added sample instructor");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

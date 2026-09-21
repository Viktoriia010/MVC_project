using Microsoft.EntityFrameworkCore;
using MvcProject.Models;
using Shop.Application.Interfaces.Configurations;
using Shop.Application.Interfaces.Helpers;
using Shop.Application.Interfaces.Repository;
using Shop.Application.Interfaces.Services;
using Shop.Application.Mapping;
using Shop.Application.Queries.Product;
using Shop.Application.Services;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Helpers;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;
using StackExchange.Redis;

namespace MvcProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


            // добавляем контекст ApplicationContext в качестве сервиса в приложение
            //builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer(connection));
            builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connection));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            ////==================MEDIATR======================
            //builder.Services.AddMediatR(cfg =>
            //{
            //    cfg.RegisterServicesFromAssembly(typeof(GetProductByIdHandler).Assembly);
            //});
            //===================REDIS=======================
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var config = builder.Configuration.GetConnectionString("RedisServerConnection");
                return ConnectionMultiplexer.Connect(config);
            });
            // ================= AutoMapper =================
            builder.Services.AddAutoMapper(
                _ => { }, typeof(CategoryProfile).Assembly, typeof(ProductProfile).Assembly,
                typeof(UserProfile).Assembly
                );


            //================= SERVICES =================

            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ICachingService, RedisCachingService>();
            builder.Services.AddScoped<IPasswordResetTokenService, PasswordResetTokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IProductService,ProductService>();

            //============================================
            //------------------HELPERS-------------
            builder.Services.AddSingleton<IHashHelper, HashHelper>();
            //================= REPOSITORIES =================

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();

            //============================================

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

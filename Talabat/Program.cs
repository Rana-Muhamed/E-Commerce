using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using talabat.Core.Entities;
using talabat.Core.Entities.Identity;
using talabat.Core.Repositories;
using talabat.Repository;
using talabat.Repository.Data;
using talabat.Repository.Identity;
using Talabat.Errors;
using Talabat.Extensions;
using Talabat.Helpers;
using Talabat.Middlewares;

namespace Talabat
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Cofigure services


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwagerServices();
            builder.Services.AddDbContext<StoreContext>(options =>

           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
               ) ;
            builder.Services.AddDbContext<AppIdentityDBContext>(options =>

           options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
               );

            builder.Services.AddApplictaionServices();
     
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            builder.Services.AddIdentityServices(builder.Configuration);
     
            #endregion

            var app = builder.Build();
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(dbContext);

                var identityDbContext = services.GetRequiredService<AppIdentityDBContext>();
                await identityDbContext.Database.MigrateAsync();



                var userManager = services.GetRequiredService<UserManager<AppUser>>(); 
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
                
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during applying the migration");
            }
 

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}
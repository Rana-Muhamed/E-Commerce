using Microsoft.AspNetCore.Mvc;
using talabat.Core.Repositories;
using Talabat.Helpers;
using talabat.Repository;
using Talabat.Errors;
using talabat.Core;
using talabat.Core.Services;
using talabat.Services;

namespace Talabat.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplictaionServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(p => p.Value.Errors.Count() > 0)//model state is a key valuepair of errors
                     .SelectMany(p => p.Value.Errors)
                     .Select(E => E.ErrorMessage)
                     .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    { Errors = errors };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return services;
        }
    }
}

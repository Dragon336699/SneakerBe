using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;
using Sneaker_Be.Handler;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Sneaker_Be.AddTransientCollection
{

    public static class AddTransient
    {
        public static void ConfigureTransient(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetCategories, IEnumerable<Category>>, GetCategoriesHandler>();
        }

        public static void ConfigureAuthen(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = configuration["Jwt:Issuer"], // Link
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        }
    }

}

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Sneaker_Be.Dtos;
using Sneaker_Be.Handler.QueryHandler;
using Sneaker_Be.Handler.QueryHandler.ProductQuery;
using Sneaker_Be.Handler.QueryHandler.UserQuery;
using Sneaker_Be.Handler.QueryHandler.CartQuery;
using Sneaker_Be.Handler.CommandHandler.CartCommand;
using Sneaker_Be.Handler.CommandHandler.UserCommand;
using Sneaker_Be.Features.Queries.ProductQuery;
using Sneaker_Be.Features.Queries.CartQuery;
using Sneaker_Be.Features.Queries.UserQuery;
using Sneaker_Be.Features.Command.UserCommand;
using Sneaker_Be.Features.Command.CartCommand;
using Sneaker_Be.Features.Command.OrderCommand;
using Sneaker_Be.Handler.CommandHandler.OrderCommand;

namespace Sneaker_Be.AddTransientCollection
{

    public static class AddTransient
    {
        public static void ConfigureTransient(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<RegisterUserCommand, string>, RegisterUserCommandHandler>();
            services.AddTransient<IRequestHandler<AddProductToCartCommand, string>, AddProductToCartCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateCartCommand, string>, UpdateCartCommandHandler>();
            services.AddTransient<IRequestHandler<PostOrderCommand, string>, PostOrderCommandHandler>();

            services.AddTransient<IRequestHandler<GetCategories, IEnumerable<Category>>, GetCategoriesHandler>();
            services.AddTransient<IRequestHandler<GetUserByPhone, User>, GetUserByPhoneHandler>();
            services.AddTransient<IRequestHandler<GetAllProducts, AllProductDto>, GetAllProductHandler>();
            services.AddTransient<IRequestHandler<GetProductById, Product>, GetProductByIdHandler>();
            services.AddTransient<IRequestHandler<GetProductViaPrice, AllProductDto>, GetProductViaPriceHandler>();
            services.AddTransient<IRequestHandler<SearchProducts, AllProductDto>, SearchProductHandler>();
            services.AddTransient<IRequestHandler<GetCart, ProductFromCartDto>, GetCartHandler>();
        }

        public static void ConfigureAuthen(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
        }
    }

}

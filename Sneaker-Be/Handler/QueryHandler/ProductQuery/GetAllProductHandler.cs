using Dapper;
using MediatR;
using Microsoft.OpenApi.Any;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.ProductQuery;

namespace Sneaker_Be.Handler.QueryHandler.ProductQuery
{
    public class GetAllProductHandler : IRequestHandler<GetAllProducts, AllProductDto>
    {
        private readonly DapperContext _dapperContext;
        public GetAllProductHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<AllProductDto> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products";
            using (var connection = _dapperContext.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                var totalProducts = products.Count();
                return new AllProductDto
                {
                    products = products.ToList(),
                    totalProducts = totalProducts
                };
            }
        }
    }
}

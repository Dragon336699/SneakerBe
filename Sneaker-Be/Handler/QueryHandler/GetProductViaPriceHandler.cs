using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;

namespace Sneaker_Be.Handler.QueryHandler
{
    public class GetProductViaPriceHandler : IRequestHandler<GetProductViaPrice, AllProductDto>
    {
        private readonly DapperContext _dapperContext;
        public GetProductViaPriceHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<AllProductDto> Handle(GetProductViaPrice request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products WHERE price BETWEEN @minPrice AND @maxPrice";
            using (var connection = _dapperContext.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("minPrice", request.MinPrice);
                parameters.Add("maxPrice", request.MaxPrice);
                var products = await connection.QueryAsync<Product>(query, parameters);
                var totalProducts = products.Count();
                return new AllProductDto
                {
                    products = products,
                    totalProducts = totalProducts
                };
            }
        }
    }
}

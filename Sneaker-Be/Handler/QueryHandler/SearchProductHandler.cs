using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;

namespace Sneaker_Be.Handler.QueryHandler
{
    public class SearchProductHandler : IRequestHandler<SearchProducts, AllProductDto>
    {
        private readonly DapperContext _dapperContext;
        public SearchProductHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<AllProductDto> Handle(SearchProducts request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products WHERE name LIKE '%' + @SearchKey + '%';";
            using (var connection = _dapperContext.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query, new {SearchKey = request.Key});
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

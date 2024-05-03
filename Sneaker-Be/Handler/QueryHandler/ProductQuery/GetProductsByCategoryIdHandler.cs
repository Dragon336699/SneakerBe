using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.ProductQuery;

namespace Sneaker_Be.Handler.QueryHandler.ProductQuery
{
    public class GetProductsByCategoryIdHandler : IRequestHandler<GetProductsByCategoryId, AllProductDto>
    {
        private readonly DapperContext _dapperContext;
        public GetProductsByCategoryIdHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<AllProductDto> Handle(GetProductsByCategoryId request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products WHERE category_id=@CategoryId";
            using (var connection = _dapperContext.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query, new {CategoryId = request.CategoryId});
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

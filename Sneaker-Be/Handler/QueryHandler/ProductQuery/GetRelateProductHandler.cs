using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.ProductQuery;

namespace Sneaker_Be.Handler.QueryHandler.ProductQuery
{
    public class GetRelateProductHandler : IRequestHandler<GetRelateProduct, AllProductDto>
    {
        private readonly DapperContext _dapperContext;
        public GetRelateProductHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<AllProductDto> Handle(GetRelateProduct request, CancellationToken cancellationToken)
        {
            var query = "SELECT TOP(4) * FROM products WHERE category_id = (SELECT category_id FROM products WHERE id = @Id) AND id != @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var relatedProducts = await connection.QueryAsync<Product>(query, new { Id = request.Id });
                return new AllProductDto
                {
                    products = relatedProducts.ToList(),
                    totalProducts = relatedProducts.Count()
                };
            }
        }
    }
}

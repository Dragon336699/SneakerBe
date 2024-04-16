using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.ProductQuery;

namespace Sneaker_Be.Handler.QueryHandler.ProductQuery
{
    public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
    {
        private readonly DapperContext _dapperContext;
        public GetProductByIdHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products WHERE Id=@Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var product = await connection.QueryFirstOrDefaultAsync<Product>(query, new { request.Id });
                return product;
            }
        }
    }
}

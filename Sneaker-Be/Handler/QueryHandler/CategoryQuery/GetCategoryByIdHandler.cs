using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.CategoryQuery;

namespace Sneaker_Be.Handler.QueryHandler.CategoryQuery
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Category>
    {
        private readonly DapperContext _dapperContext;
        public GetCategoryByIdHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Category> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM categories WHERE id=@CategoryId";
            using (var connection = _dapperContext.CreateConnection())
            {
                var category = await connection.QueryFirstOrDefaultAsync<Category>(query, new {CategoryId = request.Id});
                if (category == null) { return null; }
                return category;
            }
        }
    }
}

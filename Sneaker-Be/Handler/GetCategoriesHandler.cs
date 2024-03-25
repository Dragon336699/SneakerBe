using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries;

namespace Sneaker_Be.Handler
{
    public class GetCategoriesHandler : IRequestHandler<GetCategories, IEnumerable<Category>>
    {
        private readonly DapperContext _dapperContext;
        public GetCategoriesHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Category>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM categories";
            using (var connection = _dapperContext.CreateConnection())
            {
                var categories = await connection.QueryAsync<Category>(query);
                return categories.ToList();
            }
        }
    }
}

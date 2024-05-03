using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.CategoryCommand;
using System.Data.SqlClient;

namespace Sneaker_Be.Handler.CommandHandler.CategoryCommand
{
    public class PostCategoryCommandHandler : IRequestHandler<PostCategoryCommand, IEnumerable<Category>>
    {
        private readonly DapperContext _dapperContext;
        public PostCategoryCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Category>> Handle(PostCategoryCommand request, CancellationToken cancellationToken)
        {
            var queryCheck = "SELECT * FROM categories WHERE name = @Name";
            var query = "INSERT INTO categories (name) VALUES (@Name) " +
                "SELECT * FROM categories";
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var categoryCheck = await connection.QueryAsync<Category>(queryCheck, new { Name = request.Name });
                    if (categoryCheck.Count() > 0)
                    {
                        return null;
                    }
                    var categories = await connection.QueryAsync<Category>(query, new { Name = request.Name });
                    return categories.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
                
            }
        }
    }
}

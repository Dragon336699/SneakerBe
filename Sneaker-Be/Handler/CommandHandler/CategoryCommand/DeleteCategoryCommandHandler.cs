using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.CategoryCommand;

namespace Sneaker_Be.Handler.CommandHandler.CategoryCommand
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, IEnumerable<Category>>
    {
        private readonly DapperContext _dapperContext;
        public DeleteCategoryCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = "DELETE FROM products WHERE category_id=@Id " +
                "DELETE FROM categories WHERE id=@Id " +
                "SELECT * FROM categories";
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var categories = await connection.QueryAsync<Category>(query, new { Id = request.Id });
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

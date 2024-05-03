using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Command.CategoryCommand;

namespace Sneaker_Be.Handler.CommandHandler.CategoryCommand
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, IEnumerable<Category>>
    {
        private readonly DapperContext _dapperContext;
        public UpdateCategoryCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE categories SET name = @Name WHERE id=@Id " +
                "SELECT * FROM categories";
            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("Name",request.Name);
                    param.Add("Id", request.Id);
                    var categories = await connection.QueryAsync<Category>(query, param);
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

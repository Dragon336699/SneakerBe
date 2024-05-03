using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.ProductCommand;
using System.Data.SqlClient;

namespace Sneaker_Be.Handler.CommandHandler.ProductCommand
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Boolean>
    {
        private readonly DapperContext _dapperContext;
        public UpdateProductCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE products SET name=@Name, price=@Price, description=@Des, updated_at=@Updated,category_id=@CategoryId, sale=@Sale " +
                "WHERE id=@ProductId ";
            var param = new DynamicParameters();
            param.Add("ProductId", request.ProductId);
            param.Add("Name", request.Product.name);
            param.Add("Price", request.Product.price);
            param.Add("Des", request.Product.description);
            param.Add("Updated", DateTime.Now);
            param.Add("CategoryId", request.Product.category_id);
            param.Add("Sale", request.Product.sale);

            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var rowAffected = await connection.ExecuteAsync(query, param);
                    if (rowAffected > 0)
                    {
                        return true;
                    }
                    return false;
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }
    }
}

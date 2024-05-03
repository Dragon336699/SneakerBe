using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.ProductCommand;
using System.Data.SqlClient;

namespace Sneaker_Be.Handler.CommandHandler.ProductCommand
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Boolean>
    {
        private readonly DapperContext _dapperContext;
        public DeleteProductCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var query = "DELETE FROM order_details WHERE product_id = @Id " +
                "DELETE FROM carts WHERE product_id = @Id " +
                "DELETE FROM product_image WHERE product_id = @Id " +
                "DELETE FROM products WHERE id = @Id";
            using (var connection = _dapperContext.CreateConnection()) {
                try
                {
                    var rowAffected = await connection.ExecuteAsync(query, new { Id = request.Id });
                    return true;
                } catch (SqlException ex)
                {
                    return false;
                }
            }
        }
    }
}

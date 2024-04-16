using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.CartCommand;

namespace Sneaker_Be.Handler.CommandHandler.CartCommand
{
    public class DeleteProductFromCartCommandHandler : IRequestHandler<DeleteProductFromCartCommand, string>
    {
        private readonly DapperContext _dapperContext;
        public DeleteProductFromCartCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var query = "DELETE FROM carts WHERE id = @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = request.Id });
                return "Xóa sản phẩm khỏi giỏ hàng thành công";
            }
        }
    }
}

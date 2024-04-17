using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.CartCommand;

namespace Sneaker_Be.Handler.CommandHandler.CartCommand
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, string>
    {
        private readonly DapperContext _dapperContext;
        public UpdateCartCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE carts SET product_id =@ProductId, quantity = @Quantity, size = @Size WHERE id = @Id";
            var param = new DynamicParameters();
            param.Add("ProductId", request.Product.product_id);
            param.Add("Quantity", request.Product.Quantity);
            param.Add("Size", request.Product.Size);
            param.Add("Id", request.Id);
            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, param);
                return "Cập nhật giỏ hàng thành công";
            }
        }
    }
}

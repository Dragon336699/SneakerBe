using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.CartCommand;

namespace Sneaker_Be.Handler.CommandHandler.CartCommand
{
    public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand, string>
    {
        private readonly DapperContext _dapperContext;
        public AddProductToCartCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
        {
            var query = "IF EXISTS (SELECT * FROM carts WHERE product_id = @ProductId AND size = @Size AND user_id = @UserId) " +
                "BEGIN UPDATE carts SET quantity = quantity + @Quantity WHERE product_id = @ProductId AND size = @Size AND user_id = @UserId END " +
                "ELSE BEGIN INSERT INTO carts (product_id,user_id, quantity,size) VALUES (@ProductId,@UserId,@Quantity,@Size) END";
            var param = new DynamicParameters();
            param.Add("ProductId", request.ProductToCart.product_id);
            param.Add("UserId", request.UserId);
            param.Add("Quantity", request.ProductToCart.Quantity);
            param.Add("Size", request.ProductToCart.Size);

            using (var connection = _dapperContext.CreateConnection())
            {
                var rowEffected = await connection.ExecuteAsync(query, param);
                if (rowEffected > 0)
                {
                    return "Thêm sản phẩm vào giỏ hàng thành công";
                }
                return "Thêm sản phẩm vào giỏ hàng thất bại";
            }
        }
    }
}

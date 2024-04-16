using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.OrderCommand;

namespace Sneaker_Be.Handler.CommandHandler.OrderCommand
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, string>
    {
        private readonly DapperContext _dapperContext;
        public PostOrderCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<string> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var query = "INSERT INTO orders (user_id, fullname, email, phone_number, address, note, order_date, status, total_money, shipping_method, shipping_date, payment_method, active) " +
                "VALUES (@UserId, @FullName, @Email, @PhoneNumber, @Address, @Note, @OrderDate, @Status, @TotalMoney, @ShippingMethod, @ShippingDate, @PaymentMethod, @Active)";
            var param = new DynamicParameters();
            param.Add("UserId", request.Order.UserId);
            param.Add("FullName", request.Order.FullName);
            param.Add("Email", request.Order.Email);
            param.Add("PhoneNumber", request.Order.PhoneNumber);
            param.Add("Address", request.Order.Address);
            param.Add("Note", request.Order.Note);
            param.Add("OrderDate", DateTime.Now);
            param.Add("Status", "pending");
            param.Add("TotalMoney", request.Order.TotalMoney);
            param.Add("ShippingMethod", request.Order.ShippingMethod);
            param.Add("ShippingDate", DateTime.Now.AddDays(3));
            param.Add("PaymentMethod", request.Order.PaymentMethod);
            param.Add("Active", 1);
            using (var connection = _dapperContext.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, param);
                if (rowAffected > 0)
                {
                    return "Đặt hàng thành công";
                } return null;
            }
        }
    }
}

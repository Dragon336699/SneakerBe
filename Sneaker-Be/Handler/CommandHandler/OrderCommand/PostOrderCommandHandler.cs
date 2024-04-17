using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.OrderCommand;
using System.Text.RegularExpressions;

namespace Sneaker_Be.Handler.CommandHandler.OrderCommand
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, int>
    {
        private readonly DapperContext _dapperContext;
        public PostOrderCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<int> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            if (!validPhoneNumber(request.Order.phone_number))
            {
                return 0;
            }
            var query = "INSERT INTO orders (user_id, fullname, email, phone_number, address, note, order_date, status, total_money, shipping_method, shipping_date, payment_method, active) " +
                "VALUES (@UserId, @FullName, @Email, @PhoneNumber, @Address, @Note, @OrderDate, @Status, @TotalMoney, @ShippingMethod, @ShippingDate, @PaymentMethod, @Active) " +
                "SELECT SCOPE_IDENTITY();";
            var param = new DynamicParameters();
            param.Add("UserId", request.Order.user_id);
            param.Add("FullName", request.Order.FullName);
            param.Add("Email", request.Order.Email);
            param.Add("PhoneNumber", request.Order.phone_number);
            param.Add("Address", request.Order.Address);
            param.Add("Note", request.Order.Note);
            param.Add("OrderDate", DateTime.Now);
            param.Add("Status", "pending");
            param.Add("TotalMoney", request.Order.total_money);
            param.Add("ShippingMethod", request.Order.shipping_method);
            param.Add("ShippingDate", DateTime.Now.AddDays(3));
            param.Add("PaymentMethod", request.Order.payment_method);
            param.Add("Active", 1);

            using (var connection = _dapperContext.CreateConnection())
            {
                var orderId = await connection.ExecuteScalarAsync<int>(query, param);

                var loopQuery = "INSERT INTO order_details (order_id, product_id, price, number_of_products, total_money,size) " +
                    "VALUES (@OrderId,@ProductId,@Price,@Quantity,@TotalMoney,@Size)";
                foreach (var item in request.Order.orders_details)
                {
                    var productQuery = "SELECT price FROM products WHERE id = @ProductId";
                    var productPrice = await connection.QueryFirstOrDefaultAsync<float>(productQuery, new {ProductId = item.product_id});
                    var detailParam = new DynamicParameters();
                    detailParam.Add("@OrderId", orderId);
                    detailParam.Add("@ProductId", item.product_id);
                    detailParam.Add("@Price", productPrice);
                    detailParam.Add("@Quantity", item.number_of_products);
                    detailParam.Add("@TotalMoney", productPrice * item.number_of_products);
                    detailParam.Add("@Size", item.size);
                    await connection.ExecuteAsync(loopQuery, detailParam);
                }
                if (orderId > 0)
                {
                    return orderId;
                } return 0;
            }
        }

        public Boolean validPhoneNumber(string number)
        {
            string motif = @"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$";
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }
    }
}

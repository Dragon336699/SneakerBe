using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.Order;

namespace Sneaker_Be.Handler.QueryHandler.Order
{
    public class GetOrderDetailHandler : IRequestHandler<GetOrderDetail, InforOrderDto>
    {
        private readonly DapperContext _dapperContext;
        public GetOrderDetailHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<InforOrderDto> Handle(GetOrderDetail request, CancellationToken cancellationToken)
        {
            var query = "SELECT o.id, o.fullname, o.email, o.phone_number, o.address, o.note, o.shipping_method, o.order_date,o.status, o.payment_method, od.id as id, od.product_id, od.price, od.order_id, od.number_of_products, od.size, od.total_money, p.id as id, p.name,p.price, p.thumbnail, p.category_id, p.sale " +
                "FROM orders o JOIN order_details od ON od.order_id = o.id JOIN products p ON p.id = od.product_id WHERE o.id = @Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var orderDictionary = new Dictionary<int, InforOrderDto>();
                await connection.QueryAsync<InforOrderDto, OrderDetail, Product, InforOrderDto>(
                    query,
                    (infoOrder, orderDetail, product) =>
                    {
                        if (!orderDictionary.TryGetValue(infoOrder.Id, out var existingOrder))
                        {
                            existingOrder = infoOrder;
                            existingOrder.order_details = new List<OrderDetail>();
                            orderDictionary.Add(existingOrder.Id, existingOrder);
                        }

                        orderDetail.Product = product;
                        existingOrder.order_details.Add(orderDetail);
                        return existingOrder;
                    },
                    new { request.Id },
                    splitOn: "id, id"
                );

                return orderDictionary.Values.FirstOrDefault();
            }
        }
    }
}

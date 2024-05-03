using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Queries.Order;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sneaker_Be.Handler.QueryHandler.Order
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, IEnumerable<HistoryOrderDto>>
    {
        private readonly DapperContext _dapperContext;
        public GetAllOrdersHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<IEnumerable<HistoryOrderDto>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            var query = "WITH FirstProduct AS ( SELECT od.order_id, MIN(od.id) AS first_product_id FROM order_details od GROUP BY od.order_id ) " +
               "SELECT o.id, o.status, o.total_money, o.order_date, COUNT(od.order_id) AS total_products, p.name as product_name, p.thumbnail FROM orders o " +
               "JOIN order_details od ON od.order_id = o.id " +
               "JOIN FirstProduct fp ON o.id = fp.order_id " +
               "JOIN order_details od2 ON od2.id = fp.first_product_id " +
               "JOIN products p ON od2.product_id = p.id " +
               "GROUP BY o.id, o.status, o.total_money, o.order_date, p.name, p.thumbnail ORDER BY o.order_date DESC";
            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QueryAsync<HistoryOrderDto>(query);
                return result.ToList();
            }
        }
    }
}

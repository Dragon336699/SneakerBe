using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Queries.Order;

namespace Sneaker_Be.Handler.QueryHandler.Order
{
    public class GetHistoryOrderHandler : IRequestHandler<GetHistoryOrder, List<HistoryOrderDto>>
    {

        private readonly DapperContext _dapperContext;
        public GetHistoryOrderHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<List<HistoryOrderDto>> Handle(GetHistoryOrder request, CancellationToken cancellationToken)
        {
            var query = "WITH FirstProduct AS ( SELECT od.order_id, MIN(od.id) AS first_product_id FROM order_details od GROUP BY od.order_id ) " +
                "SELECT o.id, o.status, o.total_money, o.order_date, COUNT(od.order_id) AS total_products, p.name as product_name, p.thumbnail FROM orders o " +
                "JOIN order_details od ON od.order_id = o.id " +
                "JOIN FirstProduct fp ON o.id = fp.order_id " + 
                "JOIN order_details od2 ON od2.id = fp.first_product_id " +
                "JOIN products p ON od2.product_id = p.id " +
                "WHERE o.user_id = @UserId GROUP BY o.id, o.status, o.total_money, o.order_date, p.name, p.thumbnail ORDER BY o.order_date DESC";
            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QueryAsync<HistoryOrderDto>(query, new { UserId = request.UserId });
                return result.ToList();
            }
        }
    }
}

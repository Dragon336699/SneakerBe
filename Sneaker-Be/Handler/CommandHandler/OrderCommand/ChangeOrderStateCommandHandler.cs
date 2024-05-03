using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Features.Command.OrderCommand;

namespace Sneaker_Be.Handler.CommandHandler.OrderCommand
{
    public class ChangeOrderStateCommandHandler : IRequestHandler<ChangeOrderStateCommand, Boolean>
    {
        private readonly DapperContext _dapperContext;
        public ChangeOrderStateCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> Handle(ChangeOrderStateCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE orders SET status = @State WHERE id=@OrderId";
            var param = new DynamicParameters();
            param.Add("State", request.State);
            param.Add("OrderId", request.OrderId);
            using (var connection = _dapperContext.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, param);
                if (rowAffected > 0) {
                    return true;
                }
                return false;
            }
        }
    }
}

using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.ProductCommand;

namespace Sneaker_Be.Handler.CommandHandler.ProductCommand
{
    public class UpdateThumbnailProductCommandHandler : IRequestHandler<UpdateThumbnailProductCommand, Boolean>
    {
        private readonly DapperContext _dapperContext;
        public UpdateThumbnailProductCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> Handle(UpdateThumbnailProductCommand request, CancellationToken cancellationToken)
        {
            var query = "UPDATE products SET thumbnail = ( SELECT TOP 1 image_url FROM product_image WHERE product_id = @ProductId ) " +
                "WHERE id = @ProductId";
            using (var connection = _dapperContext.CreateConnection())
            {
                int rowAffected = await connection.ExecuteAsync(query,  new { ProductId = request.ProductId });
                if (rowAffected > 0)
                {
                    return true;
                } return false;
            }
        }
    }
}

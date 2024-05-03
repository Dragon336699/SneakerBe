using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.ProductCommand;

namespace Sneaker_Be.Handler.CommandHandler.ProductCommand
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand, Boolean>
    {
        private readonly DapperContext _dapperContext;
        public UploadProductImageCommandHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<bool> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var query = "DELETE FROM product_image WHERE product_id=@ProductId " +
                "INSERT INTO product_image (product_id, image_url) " +
                "VALUES (@ProductId, @ImageUrl)";
            var param = new DynamicParameters();
            param.Add("ProductId", request.Id);
            param.Add("ImageUrl", request.ImageUrl);
            using (var connection = _dapperContext.CreateConnection())
            {
                var rowAffected = await connection.ExecuteAsync(query, param);
                if (rowAffected > 0)
                {
                    return true;
                } return false;
            }
        }
    }
}

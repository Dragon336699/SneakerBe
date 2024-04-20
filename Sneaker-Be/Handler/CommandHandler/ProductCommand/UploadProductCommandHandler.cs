using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Features.Command.ProductCommand;
using System.Data.SqlClient;

namespace Sneaker_Be.Handler.CommandHandler.ProductCommand
{
    public class UploadProductCommandHandler : IRequestHandler<UploadProductCommand, int>
    {
        private readonly DapperContext _dapperContext;
        public UploadProductCommandHandler(DapperContext dapperContext)
        {
           _dapperContext = dapperContext;
        }
        public async Task<int> Handle(UploadProductCommand request, CancellationToken cancellationToken)
        {
            var query = "INSERT INTO products (name, price, description, created_at, updated_at,category_id, sale) " +
                "VALUES (@Name, @Price, @Des, @Created, @Updated, @CategoryId, @Sale) " +
                "SELECT SCOPE_IDENTITY();";
            var param = new DynamicParameters();
            param.Add("Name", request.Product.name);
            param.Add("Price", request.Product.price);
            param.Add("Des", request.Product.description);
            param.Add("Created", DateTime.Now);
            param.Add("Updated", DateTime.Now);
            param.Add("CategoryId", request.Product.category_id);
            param.Add("Sale", request.Product.sale);

            using (var connection = _dapperContext.CreateConnection())
            {
                try
                {
                    var productId = await connection.ExecuteScalarAsync<int>(query, param);
                    return productId;
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
        }
    }
}

using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.ProductQuery;

namespace Sneaker_Be.Handler.QueryHandler.ProductQuery
{
    public class GetProductByIdHandler : IRequestHandler<GetProductById, Product>
    {
        private readonly DapperContext _dapperContext;
        public GetProductByIdHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<Product> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM products p LEFT JOIN product_image pi ON pi.product_id = p.id  WHERE p.id=@Id";
            using (var connection = _dapperContext.CreateConnection())
            {
                var lookup = new Dictionary<int, Product>();
                var products = await connection.QueryAsync<Product, ProductImage, Product>(
                    query,
                    (product, productImage) =>
                    {
                        if (!lookup.TryGetValue(product.Id, out var foundProduct))
                        {
                            foundProduct = product;
                            foundProduct.product_images = new List<ProductImage>();
                            lookup.Add(foundProduct.Id, foundProduct);
                        }
                        foundProduct.product_images.Add(productImage);
                        return foundProduct;
                    },
                    new { Id = request.Id },
                    splitOn: "id"
                );

                return lookup.Values.FirstOrDefault();
            }
        }
    }
}

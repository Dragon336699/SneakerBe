using Dapper;
using MediatR;
using Sneaker_Be.Context;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;
using Sneaker_Be.Features.Queries.CartQuery;

namespace Sneaker_Be.Handler.QueryHandler.CartQuery
{
    public class GetCartHandler : IRequestHandler<GetCart, ProductFromCartDto>
    {
        private readonly DapperContext _dapperContext;
        public GetCartHandler(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<ProductFromCartDto> Handle(GetCart request, CancellationToken cancellationToken)
        {
            var query = "SELECT p.id, p.name, p.price, p.thumbnail, p.description, p.created_at as CreatedAt, p.updated_at as UpdateAt, p.category_id as CategoryId, p.sale,c.quantity,c.size, c.id FROM products p " +
                "INNER JOIN carts c ON c.product_id = p.id WHERE c.user_id =@UserId;";
            using (var connection = _dapperContext.CreateConnection())
            {
                var carts = await connection.QueryAsync<Product, ProductInCartDto, ProductInCartDto>(query, (product, productInCart) =>
                {
                    productInCart.products = product;
                    return productInCart;
                },
                new { request.UserId },
                splitOn: "quantity"
                );
                var totalItems = carts.Count();
                return new ProductFromCartDto
                {
                    carts = carts.ToList(),
                    totalCartItems = totalItems
                };
            }
        }
    }
}

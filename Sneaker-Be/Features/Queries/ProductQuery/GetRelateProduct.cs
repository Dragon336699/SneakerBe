using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class GetRelateProduct : IRequest<AllProductDto>
    {
        public int Id { get; set; }
        public GetRelateProduct(int id)
        {
            Id = id;
        }
    }
}

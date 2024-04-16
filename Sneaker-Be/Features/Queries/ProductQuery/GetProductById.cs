using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class GetProductById : IRequest<Product>
    {
        public int Id { get; set; }
        public GetProductById(int id)
        {
            Id = id;
        }
    }
}

using MediatR;
using Sneaker_Be.Dtos;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class GetAllProducts : IRequest<AllProductDto>
    {
    }
}

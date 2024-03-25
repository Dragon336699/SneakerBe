using MediatR;
using Sneaker_Be.Entities;
using System.Collections;

namespace Sneaker_Be.Features.Queries
{
    public class GetCategories : IRequest<IEnumerable<Category>>
    {
    }
}

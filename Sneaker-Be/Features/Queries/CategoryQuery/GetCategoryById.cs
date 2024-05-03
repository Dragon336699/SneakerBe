using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Queries.CategoryQuery
{
    public class GetCategoryById : IRequest<Category>
    {
        public int Id { get; set; }
        public GetCategoryById(int id)
        {
            Id = id;
        }
    }
}

using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class GetProductsByCategoryId : IRequest<AllProductDto>
    {
        public int CategoryId {  get; set; }
        public GetProductsByCategoryId(int categoryId)
        {   
            CategoryId = categoryId;
        }
    }
}

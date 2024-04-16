using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class GetProductViaPrice : IRequest<AllProductDto>
    {
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public GetProductViaPrice(float minPrice, float maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}

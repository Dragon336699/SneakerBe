using MediatR;
using Sneaker_Be.Dtos;

namespace Sneaker_Be.Features.Queries.ProductQuery
{
    public class SearchProducts : IRequest<AllProductDto>
    {
        public string Key { get; set; }
        public SearchProducts(string key)
        {
            Key = key;
        }
    }
}

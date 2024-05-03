using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Command.CategoryCommand
{
    public class PostCategoryCommand : IRequest<IEnumerable<Category>>
    {
        public string Name { get; set; }
        public PostCategoryCommand(string name)
        {
            Name = name;
        }
    }
}

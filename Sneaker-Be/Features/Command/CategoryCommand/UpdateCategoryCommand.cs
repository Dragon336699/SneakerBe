using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Command.CategoryCommand
{
    public class UpdateCategoryCommand : IRequest<IEnumerable<Category>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateCategoryCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

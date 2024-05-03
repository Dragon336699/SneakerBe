using MediatR;
using Sneaker_Be.Entities;

namespace Sneaker_Be.Features.Command.CategoryCommand
{
    public class DeleteCategoryCommand : IRequest<IEnumerable<Category>>
    {
        public int Id { get; set; }
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}

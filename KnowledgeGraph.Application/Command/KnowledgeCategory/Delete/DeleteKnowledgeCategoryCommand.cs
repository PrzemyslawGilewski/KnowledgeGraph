using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeCategoryCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeCategoryCommand(int id)
        {
            Id = id;
        }
    }
}

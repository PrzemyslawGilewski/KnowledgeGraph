using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeSourceTypesCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeSourceTypesCommand(int id)
        {
            Id = id;
        }
    }
}

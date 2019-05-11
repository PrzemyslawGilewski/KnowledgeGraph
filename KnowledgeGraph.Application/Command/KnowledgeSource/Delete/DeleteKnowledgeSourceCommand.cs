using MediatR;

namespace KnowledgeGraph.Application.Command
{

    public class DeleteKnowledgeSourceCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeSourceCommand(int id)
        {
            Id = id;
        }
    }
}

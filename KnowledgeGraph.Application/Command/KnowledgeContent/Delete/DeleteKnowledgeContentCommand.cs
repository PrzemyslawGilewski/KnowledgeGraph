using MediatR;

namespace KnowledgeGraph.Application.Command
{

    public class DeleteKnowledgeContentCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeContentCommand(int id)
        {
            Id = id;
        }
    }
}

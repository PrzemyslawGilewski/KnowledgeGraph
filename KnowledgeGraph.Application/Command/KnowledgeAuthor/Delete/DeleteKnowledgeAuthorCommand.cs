using MediatR;

namespace KnowledgeGraph.Application.Command
{

    public class DeleteKnowledgeAuthorCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeAuthorCommand(int id)
        {
            Id = id;
        }
    }
}

using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeAuthorByIdRequest : IRequest<KnowledgeAuthorDto>
    {
        public int Id { get; }

        public GetKnowledgeAuthorByIdRequest(int id)
        {
            Id = id;
        }
    }
}

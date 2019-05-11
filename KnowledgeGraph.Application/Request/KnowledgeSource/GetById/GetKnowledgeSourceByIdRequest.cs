using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeSourceByIdRequest : IRequest<KnowledgeSourceDto>
    {
        public int Id { get; }

        public GetKnowledgeSourceByIdRequest(int id)
        {
            Id = id;
        }
    }
}

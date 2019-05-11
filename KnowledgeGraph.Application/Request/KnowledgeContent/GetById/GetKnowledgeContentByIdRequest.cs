using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeContentByIdRequest : IRequest<KnowledgeContentDto>
    {
        public int Id { get; }

        public GetKnowledgeContentByIdRequest(int id)
        {
            Id = id;
        }
    }
}

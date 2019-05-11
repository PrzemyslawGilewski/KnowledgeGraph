using MediatR;

namespace KnowledgeGraph.Application.Request
{

    public class GetKnowledgeSourceTypeByIdRequest : IRequest<KnowledgeSourceTypeDto>
    {
        public int Id { get; }

        public GetKnowledgeSourceTypeByIdRequest(int id)
        {
            Id = id;
        }
    }
}

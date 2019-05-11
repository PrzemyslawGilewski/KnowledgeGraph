using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeConceptByIdRequest : IRequest<KnowledgeConceptDto>
    {
        public int Id { get; }

        public GetKnowledgeConceptByIdRequest(int id)
        {
            Id = id;
        }
    }
}

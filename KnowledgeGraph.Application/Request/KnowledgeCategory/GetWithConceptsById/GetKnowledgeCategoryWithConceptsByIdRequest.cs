using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeCategoryWithConceptsByIdRequest : IRequest<KnowledgeCategoryWithConceptsDto>
    {
        public int Id { get; }

        public GetKnowledgeCategoryWithConceptsByIdRequest(int id)
        {
            Id = id;
        }
    }
}

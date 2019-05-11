using MediatR;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeCategoryByIdRequest : IRequest<KnowledgeCategoryDto>
    {
        public GetKnowledgeCategoryByIdRequest(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}

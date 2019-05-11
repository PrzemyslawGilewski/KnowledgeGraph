using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeCategoriesRequest : IRequest<IEnumerable<KnowledgeCategoryDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeCategoriesRequest(string userId)
        {
            UserId = userId;
        }
    }
}

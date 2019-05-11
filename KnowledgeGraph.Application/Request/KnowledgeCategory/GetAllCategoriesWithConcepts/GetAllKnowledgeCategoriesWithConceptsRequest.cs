using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
 

    public class GetAllKnowledgeCategoriesWithConceptsRequest : IRequest<IEnumerable<KnowledgeCategoryWithConceptsDto>>
    {
        public string UserId { get; }
        public bool WithEmptyCategory { get; }

        public GetAllKnowledgeCategoriesWithConceptsRequest(string userId, bool withEmptyCategory)
        {
            UserId = userId;
            WithEmptyCategory = withEmptyCategory;
        }
    }
}

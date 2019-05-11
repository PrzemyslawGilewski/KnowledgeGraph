using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{

    public class GetAllKnowledgeContentOutConceptRequest : IRequest<IEnumerable<KnowledgeContentDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeContentOutConceptRequest(string userId)
        {
            UserId = userId;
        }
    }
}

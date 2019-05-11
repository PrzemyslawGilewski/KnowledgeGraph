using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeContentByUserIdRequest : IRequest<IEnumerable<KnowledgeContentInConceptDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeContentByUserIdRequest(string userId)
        {
            UserId = userId;
        }
    }
}

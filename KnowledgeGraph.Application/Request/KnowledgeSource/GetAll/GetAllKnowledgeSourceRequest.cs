using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeSourceRequest : IRequest<IEnumerable<KnowledgeSourceDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeSourceRequest(string userId)
        {
            UserId = userId;
        }
    }
}

using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeSourceTypesRequest : IRequest<IEnumerable<KnowledgeSourceTypeDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeSourceTypesRequest(string userId)
        {
            UserId = userId;
        }
    }
}

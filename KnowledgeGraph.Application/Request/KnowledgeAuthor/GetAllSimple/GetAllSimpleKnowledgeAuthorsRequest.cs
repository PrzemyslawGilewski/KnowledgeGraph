using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllSimpleKnowledgeAuthorsRequest : IRequest<IEnumerable<KnowledgeAuthorSimpleDto>>
    {
        public string UserId { get; }

        public GetAllSimpleKnowledgeAuthorsRequest(string userId)
        {
            UserId = userId;
        }
    }
}

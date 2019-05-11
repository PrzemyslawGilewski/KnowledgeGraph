using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeAuthorsRequest : IRequest<IEnumerable<KnowledgeAuthorDto>>
    {
        public string UserId { get; }

        public GetAllKnowledgeAuthorsRequest(string userId)
        {
            UserId = userId;
        }
    }
}

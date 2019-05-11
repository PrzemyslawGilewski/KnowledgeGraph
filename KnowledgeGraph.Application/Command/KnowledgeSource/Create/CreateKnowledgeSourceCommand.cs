using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeSourceCommand : IRequest<Response<KnowledgeSourceDto>>
    {
        public string Name { get; }
        public int? SourceTypeId { get; }
        public List<int> AuthorIds { get; }
        public string Comment { get; }
        public string UserId { get; }

        public CreateKnowledgeSourceCommand(string name, int sourceTypeId, List<int> authorIds, string comment, string userId)
        {
            Name = name;
            SourceTypeId = sourceTypeId;
            AuthorIds = authorIds;
            Comment = comment;
            UserId = userId;
        }
    }
}

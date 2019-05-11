using MediatR;
using System.Collections.Generic;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeSourceCommand : IRequest<Response<KnowledgeSourceDto>>
    {
        public int Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public int SourceTypeId { get; }
        public List<int> AuthorIds { get; }
        public string UserId { get; }

        public UpdateKnowledgeSourceCommand(int id, string name, string comment, int sourceTypeId, List<int> authorIds, string userId)
        {
            Id = id;
            Name = name;
            Comment = comment;
            SourceTypeId = sourceTypeId;
            AuthorIds = authorIds;
            UserId = userId;
        }
    }
}

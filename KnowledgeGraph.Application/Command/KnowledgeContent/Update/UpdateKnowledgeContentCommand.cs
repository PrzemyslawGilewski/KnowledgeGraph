using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeContentCommand : IRequest<Response<KnowledgeContentDto>>
    {
        public int Id { get; }
        public string Content { get; }
        public string Comment { get; }
        public string UserId { get; }
        public int? ConceptId { get; }
        public int? SourceId { get; }

        public UpdateKnowledgeContentCommand(int id, string content, string comment, string userId, int? conceptId, int? sourceId)
        {
            Id = id;
            Content = content;
            Comment = comment;
            UserId = userId;
            ConceptId = conceptId;
            SourceId = sourceId;
        }
    }
}

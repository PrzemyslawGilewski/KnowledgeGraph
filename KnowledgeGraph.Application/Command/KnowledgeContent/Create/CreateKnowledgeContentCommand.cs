using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeContentCommand : IRequest<Response<KnowledgeContentDto>>
    {
        public string Content { get; }
        public string Comment { get; }
        public string UserId { get; }
        public int? ConceptId { get; }
        public int? SourceId { get; }
   
        public CreateKnowledgeContentCommand(string content, string comment, string userId, int? conceptId = null, int? sourceId = null)
        {
            ConceptId = conceptId;
            SourceId = sourceId;
            Content = content;
            Comment = comment;
            UserId = userId;
        }
    }
}

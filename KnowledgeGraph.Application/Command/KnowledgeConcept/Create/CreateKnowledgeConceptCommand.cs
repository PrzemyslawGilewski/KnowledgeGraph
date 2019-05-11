using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeConceptCommand : IRequest<Response<KnowledgeConceptDto>> 
    {
        public string Name { get; }
        public string Comment { get; }
        public int? CategoryId { get; }
        public string UserId { get; }

        public CreateKnowledgeConceptCommand(string name, string comment, int? categoryId, string userId)
        {
            Name = name;
            Comment = comment;
            CategoryId = categoryId;
            UserId = userId;
        }
    }
}

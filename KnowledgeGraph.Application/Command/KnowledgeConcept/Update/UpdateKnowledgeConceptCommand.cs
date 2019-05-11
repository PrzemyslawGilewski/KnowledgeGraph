using MediatR;

namespace KnowledgeGraph.Application.Command
{

    public class UpdateKnowledgeConceptCommand : IRequest<Response<KnowledgeConceptDto>> 
    {
        public int Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public int? CategoryId { get; }
        public string UserId { get; }

        public UpdateKnowledgeConceptCommand(int id, string name, string comment, int categoryId, string userId)
        {
            Id = id;
            Name = name;
            Comment = comment;
            CategoryId = categoryId;
            UserId = userId;
        }
    }

}

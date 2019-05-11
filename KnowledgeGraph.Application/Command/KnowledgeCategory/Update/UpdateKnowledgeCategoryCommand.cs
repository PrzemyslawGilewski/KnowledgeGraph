using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeCategoryCommand : IRequest<Response<KnowledgeCategoryDto>>
    {
        public int Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public string UserId { get; }

        public UpdateKnowledgeCategoryCommand(int id, string name, string comment, string userId)
        {
            Id = id;
            Name = name;
            Comment = comment;
            UserId = userId;
        }
    }
}

using MediatR;

namespace KnowledgeGraph.Application.Command
{

    public class CreateKnowledgeCategoryCommand : IRequest<Response<KnowledgeCategoryDto>>
    {
        public string Name { get; }
        public string Comment { get; }
        public string UserId { get; }

        public CreateKnowledgeCategoryCommand(string name, string comment, string userId)
        {
            Name = name;
            Comment = comment;
            UserId = userId;
        }
    }
}

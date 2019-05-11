using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeSourceTypeCommand : IRequest<Response<KnowledgeSourceTypeDto>>
    {
        public string Name { get; }
        public string UserId { get; }
        public string Comment { get; }

        public CreateKnowledgeSourceTypeCommand(string name, string comment, string userId)
        {
            Name = name;
            UserId = userId;
            Comment = comment;
        }
    }
}

using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeSourceTypeCommand : IRequest<Response<KnowledgeSourceTypeDto>>
    {
        public int Id { get; }
        public string Name { get; }
        public string Comment { get; }
        public string UserId { get; }

        public UpdateKnowledgeSourceTypeCommand(int id, string name, string comment, string userId)
        {
            Id = id;
            Name = name;
            Comment = comment;
            UserId = userId;
        }
    }
}

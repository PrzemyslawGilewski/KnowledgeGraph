using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeAuthorCommand : IRequest<Response<KnowledgeAuthorDto>>
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Comment { get; }
        public string UserId { get; }

        public UpdateKnowledgeAuthorCommand(int id, string firstName, string lastName, string comment, string userId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Comment = comment;
            UserId = userId;
        }
    }
}

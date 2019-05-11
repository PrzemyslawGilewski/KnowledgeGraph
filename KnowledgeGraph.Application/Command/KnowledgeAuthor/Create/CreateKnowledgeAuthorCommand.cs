using MediatR;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeAuthorCommand : IRequest<Response<KnowledgeAuthorDto>>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Comment { get; }
        public string UserId { get; }

        public CreateKnowledgeAuthorCommand(string firstName,string lastName, string comment, string userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Comment = comment;
            UserId = userId;
        }
    }
}

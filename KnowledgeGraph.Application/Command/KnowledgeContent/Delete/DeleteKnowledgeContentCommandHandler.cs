using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeContentCommandHandler : IRequestHandler<DeleteKnowledgeContentCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeContentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(DeleteKnowledgeContentCommand request, CancellationToken cancellationToken)
        {
            var knowledgeContent = _dbContext.KnowledgeContents.FirstOrDefault(kc => kc.Id == request.Id);

            if (knowledgeContent == null)
            {
                return Response<bool>.Fail("The requested object was not found.");
            }

            _dbContext.Attach(knowledgeContent);
            _dbContext.Entry(knowledgeContent).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Response<bool>.Ok(true);

        }

    }
}

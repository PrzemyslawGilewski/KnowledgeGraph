using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeSourceCommandHandler : IRequestHandler<DeleteKnowledgeSourceCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeSourceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Response<bool>> Handle(DeleteKnowledgeSourceCommand request, CancellationToken cancellationToken)
        {
            var knowledgeSource = _dbContext.KnowledgeSources.Include(ks=>ks.Contents).FirstOrDefault(kc => kc.Id == request.Id);

            if (knowledgeSource == null)
            {
                return Response<bool>.Fail("The requested object was not found.");
            }

            if (knowledgeSource.Contents.Count() > 0)
            {
                return Response<bool>.Fail("You can not delete a Source with associated Contents.");
            }
            _dbContext.Attach(knowledgeSource);
            _dbContext.Entry(knowledgeSource).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Response<bool>.Ok(true);

        }

    }
}

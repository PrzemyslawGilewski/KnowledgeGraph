using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeAuthorCommandHandler : IRequestHandler<DeleteKnowledgeAuthorCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeAuthorCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Response<bool>> Handle(DeleteKnowledgeAuthorCommand request, CancellationToken cancellationToken)
        {
            var knowledgeAuthor = _dbContext.KnowledgeAuthors.Include(kc => kc.AuthorSource).ThenInclude(kas=>kas.Source).FirstOrDefault(kc => kc.Id == request.Id);

            if (knowledgeAuthor == null)
            {
                return Response<bool>.Fail("The requested object was not found.");
            }

            if (knowledgeAuthor.AuthorSource.Select(autSour => autSour.Source).Count() > 0)
            {
                return Response<bool>.Fail("You can not delete an Author with associated Sources.");
            }
            _dbContext.Attach(knowledgeAuthor);
            _dbContext.Entry(knowledgeAuthor).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Response<bool>.Ok(true);

        }

    }
}

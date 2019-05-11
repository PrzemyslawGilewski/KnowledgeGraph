using KnowledgeGraph.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    internal class DeleteKnowledgeSourceTypesCommandHandler : IRequestHandler<DeleteKnowledgeSourceTypesCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeSourceTypesCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(DeleteKnowledgeSourceTypesCommand request, CancellationToken cancellationToken)
        {
            var knowledgeSourceType = _dbContext.KnowledgeSourceTypes.FirstOrDefault(kst => kst.Id == request.Id);
            if (knowledgeSourceType != null)
            {
                if (knowledgeSourceType.Sources.Count > 0)
                {
                    return Response<bool>.Fail("You can not delete a Source Type with associated Contents.");
                }
                else
                {
                    _dbContext.Attach(knowledgeSourceType);
                    _dbContext.Entry(knowledgeSourceType).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    await _dbContext.SaveChangesAsync();
                    return Response<bool>.Ok(true);
                }


            }
            else
            {
                return Response<bool>.Fail("Brak obiektu w bazie.");
            }
        }


    }
}

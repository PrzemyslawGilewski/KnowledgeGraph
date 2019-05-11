using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    class DeleteKnowledgeConceptCommandHandler : IRequestHandler<DeleteKnowledgeConceptCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeConceptCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(DeleteKnowledgeConceptCommand request, CancellationToken cancellationToken)
        {
            var concept = _dbContext.KnowledgeConcepts.Include(kc => kc.Contents).FirstOrDefault(kc => kc.Id == request.Id);

            if (concept == null)
            {
                return Response<bool>.Fail("The requested object was not found.");
            }

            if (concept.Contents.Count() > 0)
            {
                return Response<bool>.Fail("You can not delete a Concept with associated Contents.");
            }
            _dbContext.Attach(concept);
            _dbContext.Entry(concept).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return Response<bool>.Ok(true);
        }
    }
}

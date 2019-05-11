using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
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
    public class DeleteKnowledgeCategoryCommandHandler : IRequestHandler<DeleteKnowledgeCategoryCommand, Response<bool>>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKnowledgeCategoryCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response<bool>> Handle(DeleteKnowledgeCategoryCommand request, CancellationToken cancellationToken)
        {
            var knowledgeCategory = _dbContext.KnowledgeCategories.Include(kc => kc.KnowledgeConcepts).FirstOrDefault(kc => kc.Id == request.Id);

            if (knowledgeCategory == null)
            {
                return Response<bool>.Fail("The requested object was not found");
            }

            if (knowledgeCategory.KnowledgeConcepts.Count() > 0)
            {
                return Response<bool>.Fail("You can not delete a Category with associated Concepts.");
            }
            _dbContext.Attach(knowledgeCategory);
            _dbContext.Entry(knowledgeCategory).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return Response<bool>.Ok(true);

        }

    }
}

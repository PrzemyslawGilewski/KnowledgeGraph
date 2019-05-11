using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    internal class UpdateKnowledgeContentCommandHandler : IRequestHandler<UpdateKnowledgeContentCommand, Response<KnowledgeContentDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateKnowledgeContentCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeContentDto>> Handle(UpdateKnowledgeContentCommand request, CancellationToken cancellationToken)
        {
            int? sourceId = null;

            if (request.SourceId != null && request.SourceId != 0)
            {
                sourceId = request.SourceId;
            }

            int? conceptId = null;

            if (request.ConceptId != null && request.ConceptId != 0)
            {
                conceptId = request.ConceptId;
            }

            var knowledgeContent = _dbContext.KnowledgeContents.FirstOrDefault(kc => kc.Id == request.Id);
            if (knowledgeContent == null)
            {
                return Response<KnowledgeContentDto>.Fail("The requested object was not found.");
            }

            knowledgeContent.Content = request.Content;
            knowledgeContent.Comment = request.Comment;
            knowledgeContent.LastModificationDate = DateTime.Now;
            knowledgeContent.SourceId = sourceId;
            knowledgeContent.ConceptId = conceptId;

            _dbContext.Entry(knowledgeContent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Response<KnowledgeContentDto>.Ok(_mapper.Map<KnowledgeContentDto>(knowledgeContent));

        }
    }
}

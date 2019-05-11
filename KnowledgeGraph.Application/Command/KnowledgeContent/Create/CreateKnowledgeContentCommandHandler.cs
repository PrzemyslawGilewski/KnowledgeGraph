using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    internal class CreateKnowledgeContentCommandHandler : IRequestHandler<CreateKnowledgeContentCommand, Response<KnowledgeContentDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeContentCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeContentDto>> Handle(CreateKnowledgeContentCommand request, CancellationToken cancellationToken)
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

            var knowledgeContent = new KnowledgeContent()
            {
                SourceId = sourceId,
                ConceptId = conceptId,
                Content = request.Content,
                Comment = request.Comment,
                CreationDate = DateTime.Now,
                LastModificationDate = DateTime.Now,
                UserId = request.UserId
            };

            var result = await _dbContext.KnowledgeContents.AddAsync(knowledgeContent);
            await _dbContext.SaveChangesAsync();

            return Response<KnowledgeContentDto>.Ok(_mapper.Map<KnowledgeContentDto>(result.Entity));
        }
    }
}

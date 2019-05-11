using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    internal class UpadteKnowledgeSourceRequestHandler : IRequestHandler<UpdateKnowledgeSourceCommand, Response<KnowledgeSourceDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpadteKnowledgeSourceRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeSourceDto>> Handle(UpdateKnowledgeSourceCommand request, CancellationToken cancellationToken)
        {
            bool sourceWithParticularNameExists = _dbContext.KnowledgeSources.Where(ks => ks.UserId == request.UserId && ks.Id != request.Id).Any(kc => kc.Name == request.Name);

            if (sourceWithParticularNameExists)
            {
                return Response<KnowledgeSourceDto>.Fail("A Source with this name already exists.");
            }

            var knowledgeSource = _dbContext.KnowledgeSources.Include(ks => ks.AuthorSource).FirstOrDefault(kc => kc.Id == request.Id);
            if (knowledgeSource == null)
            {
                return Response<KnowledgeSourceDto>.Fail("The requested object was not found.");
            }

            knowledgeSource.Name = request.Name;
            knowledgeSource.LastModificationTime = DateTime.Now;
            knowledgeSource.TypeId = request.SourceTypeId;
            knowledgeSource.Comment = request.Comment;

            _dbContext.Set<KnowledgeAuthorSource>().RemoveRange(knowledgeSource.AuthorSource);

            if (request.AuthorIds != null)
            {
                foreach (var authorId in request.AuthorIds)
                {
                    _dbContext.Set<KnowledgeAuthorSource>().Add(new KnowledgeAuthorSource
                    {
                        SourceId = knowledgeSource.Id,
                        AuthorId = authorId
                    });
                }
            }

            _dbContext.Entry(knowledgeSource).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
            return Response<KnowledgeSourceDto>.Ok(_mapper.Map<KnowledgeSourceDto>(knowledgeSource));
        }
    }
}

using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    class CreateKnowledgeSourceCommandHandler : IRequestHandler<CreateKnowledgeSourceCommand, Response<KnowledgeSourceDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeSourceCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeSourceDto>> Handle(CreateKnowledgeSourceCommand request, CancellationToken cancellationToken)
        {
            bool nameExists = _dbContext.KnowledgeSources.Any(ks => ks.Name == request.Name);

            if (nameExists)
            {
                return Response<KnowledgeSourceDto>.Fail("A Source with this name already exists.");
            }
            else
            {
                int? sourceTypeId = null;

                if (request.SourceTypeId != null && request.SourceTypeId != 0)
                {
                    sourceTypeId = request.SourceTypeId;
                }

                var source = new KnowledgeSource()
                {
                    Name = request.Name,
                    TypeId = sourceTypeId,
                    Comment = request.Comment,
                    UserId = request.UserId,
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now
                };

                var result = _dbContext.KnowledgeSources.Add(source);

                if (request.AuthorIds != null)
                {
                    source.AuthorSource = request.AuthorIds.Select(x => new KnowledgeAuthorSource { SourceId = source.Id, AuthorId = x }).ToList();
                }

                await _dbContext.SaveChangesAsync();

                return Response<KnowledgeSourceDto>.Ok(_mapper.Map<KnowledgeSourceDto>(result.Entity));
            }
        }


    }
}

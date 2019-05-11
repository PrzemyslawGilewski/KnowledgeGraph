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
    internal class CreateKnowledgeSourceTypeCommandHandler : IRequestHandler<CreateKnowledgeSourceTypeCommand, Response<KnowledgeSourceTypeDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeSourceTypeCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeSourceTypeDto>> Handle(CreateKnowledgeSourceTypeCommand request, CancellationToken cancellationToken)
        {
            bool unitNameExists = _dbContext.KnowledgeSourceTypes.Any(kst => kst.Name == request.Name);

            if (unitNameExists)
            {
                return Response<KnowledgeSourceTypeDto>.Fail("The Source Type with this name already exists.");
            }
            else
            {
                var result = _dbContext.KnowledgeSourceTypes.Add(new KnowledgeSourceType()
                {
                    Name = request.Name,
                    Comment = request.Comment,
                    UserId = request.UserId,
                    CreationTime = DateTime.Now,
                    LastModificationTime = DateTime.Now
                });
                var iResult = await _dbContext.SaveChangesAsync();

                return Response<KnowledgeSourceTypeDto>.Ok(_mapper.Map<KnowledgeSourceTypeDto>(result.Entity));
            }
        }

    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    class GetAllKnowledgeContentForSourceRequestHandler : IRequestHandler<GetAllKnowledgeContentForSourceRequest, KnowledgeSourceWithContentDto>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeContentForSourceRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<KnowledgeSourceWithContentDto> Handle(GetAllKnowledgeContentForSourceRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.KnowledgeContents
                .Include(kc => kc.Source)
                .Where(kc => kc.SourceId == request.Id)
                .GroupBy(kc => kc.Source)
                .Select(kc => new KnowledgeSourceWithContentDto
                {
                    Source =  _mapper.Map<KnowledgeSourceDto>(kc.Key),
                    Contents = _mapper.Map<IEnumerable<KnowledgeContentDto>>(kc.Select(kc=>kc))
                })
                .FirstOrDefault();
        }
    }
}

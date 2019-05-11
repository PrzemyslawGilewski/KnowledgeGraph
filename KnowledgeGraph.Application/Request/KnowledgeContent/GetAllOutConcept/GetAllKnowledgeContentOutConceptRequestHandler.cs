using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    class GetAllKnowledgeContentOutConceptRequestHandler : IRequestHandler<GetAllKnowledgeContentOutConceptRequest, IEnumerable<KnowledgeContentDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeContentOutConceptRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeContentDto>> Handle(GetAllKnowledgeContentOutConceptRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.KnowledgeContents
                .Include(kc => kc.Source)
                .Where(kc => kc.UserId == request.UserId && kc.ConceptId == null)
                .ProjectTo<KnowledgeContentDto>(_mapper.ConfigurationProvider);
        }
    }
}

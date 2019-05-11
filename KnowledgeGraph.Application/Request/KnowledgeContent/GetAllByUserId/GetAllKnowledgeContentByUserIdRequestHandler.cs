using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{

    internal class GetAllKnowledgeContentByUserIdRequestHandler : IRequestHandler<GetAllKnowledgeContentByUserIdRequest, IEnumerable<KnowledgeContentInConceptDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAllKnowledgeContentByUserIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeContentInConceptDto>> Handle(GetAllKnowledgeContentByUserIdRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.KnowledgeContents
                .Include(kc => kc.Source)
                .Where(kc => kc.UserId == request.UserId)
                .ProjectTo<KnowledgeContentInConceptDto>(_mapper.ConfigurationProvider);
        }
    }
}

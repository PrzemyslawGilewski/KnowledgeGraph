using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request.KnowledgeSourceType.GetAll
{
    public class GetAllKnowledgeSourceTypesRequestHandler : IRequestHandler<GetAllKnowledgeSourceTypesRequest, IEnumerable<KnowledgeSourceTypeDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeSourceTypesRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeSourceTypeDto>> Handle(GetAllKnowledgeSourceTypesRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.KnowledgeSourceTypes
                .Include(kst => kst.Sources)
                .Where(kst => kst.UserId == request.UserId)
                .ProjectTo<KnowledgeSourceTypeDto>(_mapper.ConfigurationProvider);
        }
    }
}

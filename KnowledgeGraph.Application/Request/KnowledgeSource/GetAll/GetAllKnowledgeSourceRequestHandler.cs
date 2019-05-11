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
    public class GetAllKnowledgeSourceRequestHandler : IRequestHandler<GetAllKnowledgeSourceRequest, IEnumerable<KnowledgeSourceDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeSourceRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeSourceDto>> Handle(GetAllKnowledgeSourceRequest request, CancellationToken cancellationToken)
        {
            return _dbContext
                .KnowledgeSources
                .Include(ks => ks.Type)
                .Include(ks => ks.Contents)
                .Include(ks => ks.AuthorSource)
                .ThenInclude(kas => kas.Author)
                .Where(kas=>kas.UserId == request.UserId)
                .ProjectTo<KnowledgeSourceDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }
    }
}

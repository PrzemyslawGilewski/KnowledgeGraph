using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request.KnowledgeCategory.GetAllCategoriesWithConcepts
{

    public class GetAllSimpleKnowledgeAuthorsRequestHandler : IRequestHandler<GetAllSimpleKnowledgeAuthorsRequest, IEnumerable<KnowledgeAuthorSimpleDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllSimpleKnowledgeAuthorsRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeAuthorSimpleDto>> Handle(GetAllSimpleKnowledgeAuthorsRequest request, CancellationToken cancellationToken)
        {
            return _dbContext.KnowledgeAuthors
                .Where(kc => kc.UserId == request.UserId)
                .ProjectTo<KnowledgeAuthorSimpleDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }
    }
}

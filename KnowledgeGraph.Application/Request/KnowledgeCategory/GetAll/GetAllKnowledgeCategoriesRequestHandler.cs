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
    public class GetAllKnowledgeCategoriesRequestHandler : IRequestHandler<GetAllKnowledgeCategoriesRequest, IEnumerable<KnowledgeCategoryDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeCategoriesRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeCategoryDto>> Handle(GetAllKnowledgeCategoriesRequest request, CancellationToken cancellationToken)
        {
           return _dbContext.KnowledgeCategories
                .Include(kc => kc.KnowledgeConcepts)
                .Where(kc => kc.UserId == request.UserId)
                .ProjectTo<KnowledgeCategoryDto>(_mapper.ConfigurationProvider);
        }
    }
}

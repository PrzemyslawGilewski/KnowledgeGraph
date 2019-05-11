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

    public class GetAllKnowledgeCategoriesWithConceptsRequestHandler : IRequestHandler<GetAllKnowledgeCategoriesWithConceptsRequest, IEnumerable<KnowledgeCategoryWithConceptsDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeCategoriesWithConceptsRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeCategoryWithConceptsDto>> Handle(GetAllKnowledgeCategoriesWithConceptsRequest request, CancellationToken cancellationToken)
        {
            if (request.WithEmptyCategory)
            {
                return _dbContext.KnowledgeConcepts
                 .Include(kc => kc.Category)
                 .Include(kc => kc.Contents)
                 .Where(kc => kc.UserId == request.UserId)
                 .GroupBy(kc => kc.Category)
                 .Select(kc => new KnowledgeCategoryWithConceptsDto
                 {
                     Category = _mapper.Map<KnowledgeCategoryDto>(kc.Key),
                     KnowledgeConcepts = _mapper.Map<IEnumerable<KnowledgeConceptSimpleDto>>(kc.Select(kc => kc))
                 }).AsEnumerable();
            }
            else
            {
                return _dbContext.KnowledgeConcepts
                 .Include(kc => kc.Category)
                 .Include(kc => kc.Contents)
                 .Where(kc => kc.UserId == request.UserId)
                 .GroupBy(kc => kc.Category)
                 .Where(kc => kc.Key != null)
                 .Select(kc => new KnowledgeCategoryWithConceptsDto
                 {
                     Category = _mapper.Map<KnowledgeCategoryDto>(kc.Key),
                     KnowledgeConcepts = _mapper.Map<IEnumerable<KnowledgeConceptSimpleDto>>(kc.Select(kc => kc))
                 }).AsEnumerable();
            }
        }
    }
}

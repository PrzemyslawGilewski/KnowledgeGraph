using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeCategoryWithConceptsByIdRequestHandler : IRequestHandler<GetKnowledgeCategoryWithConceptsByIdRequest, KnowledgeCategoryWithConceptsDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeCategoryWithConceptsByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<KnowledgeCategoryWithConceptsDto> Handle(GetKnowledgeCategoryWithConceptsByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeCategories.Include(kc=>kc.KnowledgeConcepts).FirstOrDefault(kc => kc.Id == request.Id);
            return _mapper.Map<KnowledgeCategoryWithConceptsDto>(result);
        }
    }
}

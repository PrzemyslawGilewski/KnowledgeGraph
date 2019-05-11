using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeCategoryByIdRequestHandler : IRequestHandler<GetKnowledgeCategoryByIdRequest, KnowledgeCategoryDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeCategoryByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<KnowledgeCategoryDto> Handle(GetKnowledgeCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeCategories.FirstOrDefault(kc => kc.Id == request.Id);
            return _mapper.Map<KnowledgeCategoryDto>(result);
        }
    }
}

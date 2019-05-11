using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeGraph.Application.Request
{
    internal class GetKnowledgeSourceTypeByIdRequestHandler : IRequestHandler<GetKnowledgeSourceTypeByIdRequest, KnowledgeSourceTypeDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeSourceTypeByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<KnowledgeSourceTypeDto> Handle(GetKnowledgeSourceTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeSourceTypes.Include(kst => kst.Sources).FirstOrDefault(kst => kst.Id == request.Id);
            return _mapper.Map<KnowledgeSourceTypeDto>(result);
        }
    }
}

using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    internal class GetKnowledgeContentByIdRequestHandler : IRequestHandler<GetKnowledgeContentByIdRequest, KnowledgeContentDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeContentByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<KnowledgeContentDto> Handle(GetKnowledgeContentByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeContents.Include(kc => kc.Source).FirstOrDefault(ksc => ksc.Id == request.Id);
            return _mapper.Map<KnowledgeContentDto>(result);
        }
    }
}

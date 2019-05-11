using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeSourceByIdRequestHandler : IRequestHandler<GetKnowledgeSourceByIdRequest, KnowledgeSourceDto>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeSourceByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<KnowledgeSourceDto> Handle(GetKnowledgeSourceByIdRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<KnowledgeSourceDto>(_dbContext
                .KnowledgeSources
                .Include(ks => ks.Type)
                .Include(ks => ks.Contents)
                .Include(ks => ks.AuthorSource)
                .ThenInclude(kas => kas.Author)
                .FirstOrDefault(ks => ks.Id == request.Id));
        }
    }
}

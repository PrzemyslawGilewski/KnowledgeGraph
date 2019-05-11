using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request
{
    public class GetKnowledgeAuthorByIdRequestHandler : IRequestHandler<GetKnowledgeAuthorByIdRequest, KnowledgeAuthorDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeAuthorByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<KnowledgeAuthorDto> Handle(GetKnowledgeAuthorByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeAuthors.Include(ka=>ka.AuthorSource).ThenInclude(kas => kas.Source).FirstOrDefault(kc => kc.Id == request.Id);
            return _mapper.Map<KnowledgeAuthorDto>(result);
        }
    }
}

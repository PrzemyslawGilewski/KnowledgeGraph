using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request.KnowledgeConcept.GetById
{
    internal class GetKnowledgeConceptByIdRequestHandler : IRequestHandler<GetKnowledgeConceptByIdRequest, KnowledgeConceptDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetKnowledgeConceptByIdRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<KnowledgeConceptDto> Handle(GetKnowledgeConceptByIdRequest request, CancellationToken cancellationToken)
        {
            var result = _dbContext.KnowledgeConcepts.Include(kc=>kc.Contents).ThenInclude(kc=>kc.Source).Where(kc => kc.Id == request.Id).FirstOrDefault();
            return _mapper.Map<KnowledgeConceptDto>(result);
        }
    }
}

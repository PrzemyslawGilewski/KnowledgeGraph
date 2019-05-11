using AutoMapper;
using AutoMapper.QueryableExtensions;
using KnowledgeGraph.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Request.KnowledgeCategory.GetAllCategoriesWithConcepts
{

    public class GetAllKnowledgeAuthorsRequestHandler : IRequestHandler<GetAllKnowledgeAuthorsRequest, IEnumerable<KnowledgeAuthorDto>>
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllKnowledgeAuthorsRequestHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KnowledgeAuthorDto>> Handle(GetAllKnowledgeAuthorsRequest request, CancellationToken cancellationToken)
        {
            return  _dbContext.KnowledgeAuthors
                .Include(ka => ka.AuthorSource)
                .ThenInclude(kas => kas.Source)
                .ProjectTo<KnowledgeAuthorDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }
    }
}

using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{

    internal class CreateKnowledgeConceptCommandHandler : IRequestHandler<CreateKnowledgeConceptCommand, Response<KnowledgeConceptDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeConceptCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeConceptDto>> Handle(CreateKnowledgeConceptCommand request, CancellationToken cancellationToken)
        {
            bool unitNameExists = _dbContext.KnowledgeConcepts.Where(kc=>kc.UserId == request.UserId).Any(kc => kc.Name == request.Name);

            if (unitNameExists)
            {
                return Response<KnowledgeConceptDto>.Fail("The Concept with this name already exists.");
            }
            else
            {
                int? categoryId = null;

                if (request.CategoryId != null && request.CategoryId != 0)
                {
                    categoryId = request.CategoryId;
                }

                var result = _dbContext
                    .KnowledgeConcepts
                    .Add(new KnowledgeConcept()
                    {
                        Name = request.Name,
                        Comment = request.Comment,
                        CategoryId = categoryId,
                        UserId = request.UserId
                    });

                await _dbContext.SaveChangesAsync();

                return Response<KnowledgeConceptDto>.Ok(_mapper.Map<KnowledgeConceptDto>(result.Entity));
            }
        }


    }
}

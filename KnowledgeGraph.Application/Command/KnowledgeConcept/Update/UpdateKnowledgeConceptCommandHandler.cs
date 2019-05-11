using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    internal class UpdateKnowledgeConceptCommandHandler : IRequestHandler<UpdateKnowledgeConceptCommand, Response<KnowledgeConceptDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateKnowledgeConceptCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeConceptDto>> Handle(UpdateKnowledgeConceptCommand request, CancellationToken cancellationToken)
        {

            var knowledgeConcept = _dbContext.KnowledgeConcepts.FirstOrDefault(kc => kc.Id == request.Id);
            if (knowledgeConcept == null)
            {
                return Response<KnowledgeConceptDto>.Fail("The requested object was not found.");
            }

            int? categoryId = null;

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                categoryId = request.CategoryId;
            }

            knowledgeConcept.Name = request.Name;
            knowledgeConcept.Comment = request.Comment;
            knowledgeConcept.CategoryId = categoryId;

            _dbContext.Entry(knowledgeConcept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return Response<KnowledgeConceptDto>.Ok(_mapper.Map<KnowledgeConceptDto>(knowledgeConcept));

        }


    }
}

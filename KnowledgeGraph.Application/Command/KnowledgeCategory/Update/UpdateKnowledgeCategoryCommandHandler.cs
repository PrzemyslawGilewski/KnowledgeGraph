using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeCategoryCommandHandler : IRequestHandler<UpdateKnowledgeCategoryCommand, Response<KnowledgeCategoryDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateKnowledgeCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeCategoryDto>> Handle(UpdateKnowledgeCategoryCommand request, CancellationToken cancellationToken)
        {
            var knowledgeCategory = _dbContext.KnowledgeCategories.FirstOrDefault(kc => kc.Id == request.Id);
            if (knowledgeCategory == null)
            {
                return Response<KnowledgeCategoryDto>.Fail("The requested object was not found.");
            }

            knowledgeCategory.Name = request.Name;
            knowledgeCategory.Comment = request.Comment;
          
            _dbContext.Entry(knowledgeCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Response<KnowledgeCategoryDto>.Ok(_mapper.Map<KnowledgeCategoryDto>(knowledgeCategory));
        }
    }
}

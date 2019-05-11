using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeCategoryCommandHandler : IRequestHandler<CreateKnowledgeCategoryCommand, Response<KnowledgeCategoryDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeCategoryCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<Response<KnowledgeCategoryDto>> Handle(CreateKnowledgeCategoryCommand request, CancellationToken cancellationToken)
        {
            bool nameExists = _dbContext.KnowledgeCategories.Where(kc => kc.UserId == request.UserId).Any(kc => kc.Name == request.Name);

            if (nameExists)
            {
                return Response<KnowledgeCategoryDto>.Fail("A Category with this name already exists.");
            }


            var knowledgeCategory = new KnowledgeCategory() { Name = request.Name, Comment = request.Comment, UserId = request.UserId };

            var result = _dbContext.KnowledgeCategories.Add(knowledgeCategory);
            await _dbContext.SaveChangesAsync();
            if (result != null)
            {
                return Response<KnowledgeCategoryDto>.Ok(_mapper.Map<KnowledgeCategoryDto>(result.Entity));
            }
            else
            {
                return Response<KnowledgeCategoryDto>.Fail("An error occurred while creating the Category.");
            }
        }

    }
}

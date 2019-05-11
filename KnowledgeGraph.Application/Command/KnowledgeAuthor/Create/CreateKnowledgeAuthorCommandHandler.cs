using AutoMapper;
using KnowledgeGraph.Data;
using KnowledgeGraph.Data.Model;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class CreateKnowledgeAuthorCommandHandler : IRequestHandler<CreateKnowledgeAuthorCommand, Response<KnowledgeAuthorDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateKnowledgeAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeAuthorDto>> Handle(CreateKnowledgeAuthorCommand request, CancellationToken cancellationToken)
        {
            bool authorWithParticularFirstAndLastNameExists = _dbContext.KnowledgeAuthors.Where(kc => kc.UserId == request.UserId).Any(kc => kc.FirstName == request.FirstName && kc.LastName == request.LastName);

            if (authorWithParticularFirstAndLastNameExists)
            {
                return Response<KnowledgeAuthorDto>.Fail("An author with this name and surname already exists.");
            }

            var knowledgeAuthor = new KnowledgeAuthor()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Comment = request.Comment,
                CreationTime = DateTime.Now,
                LastModificationTime = DateTime.Now,
                UserId = request.UserId,
            };

            var result = _dbContext.KnowledgeAuthors.Add(knowledgeAuthor);
            await _dbContext.SaveChangesAsync();

            if (result != null)
            {
                return Response<KnowledgeAuthorDto>.Ok(_mapper.Map<KnowledgeAuthorDto>(result.Entity));
            }
            else
            {
                return Response<KnowledgeAuthorDto>.Fail("An error occurred while creating an Author.");
            }
        }

    }
}

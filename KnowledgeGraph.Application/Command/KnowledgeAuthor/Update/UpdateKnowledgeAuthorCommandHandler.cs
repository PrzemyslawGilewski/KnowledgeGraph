using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
    public class UpdateKnowledgeAuthorCommandHandler : IRequestHandler<UpdateKnowledgeAuthorCommand, Response<KnowledgeAuthorDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateKnowledgeAuthorCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<Response<KnowledgeAuthorDto>> Handle(UpdateKnowledgeAuthorCommand request, CancellationToken cancellationToken)
        {
            bool authorWithParticularFirstAndLastNameExists = _dbContext.KnowledgeAuthors.Where(kc => kc.UserId == request.UserId && kc.Id != request.Id).Any(kc => kc.FirstName == request.FirstName && kc.LastName == request.LastName);

            if (authorWithParticularFirstAndLastNameExists)
            {
                return Response<KnowledgeAuthorDto>.Fail("An Author with this name and surname already exists.");
            }

            var knowledgeAuthor = _dbContext.KnowledgeAuthors.FirstOrDefault(kc => kc.Id == request.Id);

            if (knowledgeAuthor == null)
            {
                return Response<KnowledgeAuthorDto>.Fail("The requested object was not found.");
            }

            knowledgeAuthor.FirstName = request.FirstName;
            knowledgeAuthor.LastName = request.LastName;
            knowledgeAuthor.LastModificationTime = DateTime.Now;
            knowledgeAuthor.Comment = request.Comment;
          
            _dbContext.Entry(knowledgeAuthor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Response<KnowledgeAuthorDto>.Ok(_mapper.Map<KnowledgeAuthorDto>(knowledgeAuthor));
        }
    }
}

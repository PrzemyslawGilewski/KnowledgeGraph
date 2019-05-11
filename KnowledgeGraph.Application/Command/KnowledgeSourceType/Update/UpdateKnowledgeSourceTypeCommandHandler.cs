using AutoMapper;
using KnowledgeGraph.Data;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnowledgeGraph.Application.Command
{
   
    internal class UpdateKnowledgeSourceTypeCommandHandler : IRequestHandler<UpdateKnowledgeSourceTypeCommand, Response<KnowledgeSourceTypeDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateKnowledgeSourceTypeCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<KnowledgeSourceTypeDto>> Handle(UpdateKnowledgeSourceTypeCommand request, CancellationToken cancellationToken)
        {
            var knowledgeSourceType = _dbContext.KnowledgeSourceTypes.FirstOrDefault(kst => kst.Id == request.Id);
            if (knowledgeSourceType != null)
            {
                knowledgeSourceType.Name = request.Name;
                knowledgeSourceType.Comment = request.Comment;
                knowledgeSourceType.LastModificationTime = DateTime.Now;

                _dbContext.Attach(knowledgeSourceType);
                _dbContext.Entry(knowledgeSourceType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                    return Response<KnowledgeSourceTypeDto>.Ok(_mapper.Map<KnowledgeSourceTypeDto>(knowledgeSourceType));
            }
            else
            {
                return Response<KnowledgeSourceTypeDto>.Fail("The requested object was not found.");
            }
        }
    }
}

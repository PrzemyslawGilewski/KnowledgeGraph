using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class GetAllKnowledgeContentForSourceRequest : IRequest<KnowledgeSourceWithContentDto>
    {
        public int Id { get; }

        public GetAllKnowledgeContentForSourceRequest(int id)
        {
            Id = id;
        }
    }
}

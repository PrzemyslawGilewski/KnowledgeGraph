using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Command
{
    public class DeleteKnowledgeConceptCommand : IRequest<Response<bool>>
    {
        public int Id { get; }

        public DeleteKnowledgeConceptCommand(int id)
        {
            Id = id;
        }
    }
}

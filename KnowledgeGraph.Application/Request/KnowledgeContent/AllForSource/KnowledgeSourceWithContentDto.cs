using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeSourceWithContentDto
    {
        public KnowledgeSourceDto Source { get; set; }
        public IEnumerable<KnowledgeContentDto> Contents { get; set; }
    }
}

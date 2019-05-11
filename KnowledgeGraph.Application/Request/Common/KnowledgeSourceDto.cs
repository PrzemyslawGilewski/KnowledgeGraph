using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeSourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public KnowledgeSourceTypeDto Type { get; set; }
        public IEnumerable<KnowledgeAuthorSimpleDto> Authors { get; set; }
        public int UsageCount { get; set; }
        public DateTime CreationTime { get; set; }

        public DateTime LastModificationTime { get; set; }
        
    }
}

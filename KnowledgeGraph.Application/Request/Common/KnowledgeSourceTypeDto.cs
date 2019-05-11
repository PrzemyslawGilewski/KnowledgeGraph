using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeSourceTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public IEnumerable<KnowledgeSourceDto> Sources { get; set; }
    }
}

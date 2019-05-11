using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Command
{
    public class KnowledgeContentDto
    {
        public int Id { get; set; }
        public KnowledgeSourceDto Source { get; set; }
        public KnowledgeConceptDto Concept { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}

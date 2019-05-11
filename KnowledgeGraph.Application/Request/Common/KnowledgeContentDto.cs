using System;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeContentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Comment { get; set; }
        public int ConceptId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public KnowledgeSourceDto Source { get; set; }
    }
}

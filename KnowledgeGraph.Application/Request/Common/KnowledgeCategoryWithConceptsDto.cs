using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeCategoryWithConceptsDto
    {
        public KnowledgeCategoryDto Category { get; set; }
        public IEnumerable<KnowledgeConceptSimpleDto> KnowledgeConcepts { get; set; }
    }
}

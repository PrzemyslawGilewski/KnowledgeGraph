using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeAuthorSource
    {
        public int AuthorId { get; set; }
        public int SourceId { get; set; }
        public KnowledgeAuthor Author { get; set; }
        public KnowledgeSource Source { get; set; }
    }
}

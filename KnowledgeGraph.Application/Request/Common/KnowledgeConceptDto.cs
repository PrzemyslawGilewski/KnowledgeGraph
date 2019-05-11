﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeGraph.Application.Request
{
    public class KnowledgeConceptDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public KnowledgeCategoryDto Category { get; set; }
        public IEnumerable<KnowledgeContentDto> Contents { get; set; }
    }
}

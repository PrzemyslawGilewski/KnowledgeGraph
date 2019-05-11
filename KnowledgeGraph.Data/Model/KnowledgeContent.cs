using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeContent
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        public string Comment { get; set; }
        public int? SourceId { get; set; }
        public KnowledgeSource Source { get; set; }
        public int? ConceptId { get; set; }
        public KnowledgeConcept Concept { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

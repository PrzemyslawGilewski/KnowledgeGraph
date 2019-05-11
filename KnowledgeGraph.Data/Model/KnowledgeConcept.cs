using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeConcept
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Comment { get; set; }
        public int? CategoryId { get; set; }
        
        public KnowledgeCategory Category { get; set; }

        public IList<KnowledgeContent> Contents { get; set; }

    }
}

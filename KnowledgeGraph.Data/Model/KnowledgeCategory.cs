using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Comment { get; set; }

        [Required]
        public string UserId { get; set; }
        public IList<KnowledgeConcept> KnowledgeConcepts { get; set; }
    }
}

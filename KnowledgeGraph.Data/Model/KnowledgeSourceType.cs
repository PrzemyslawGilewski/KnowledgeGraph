using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeSourceType
    {
        public KnowledgeSourceType()
        {
            Sources = new List<KnowledgeSource>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Comment { get; set; }
        public IList<KnowledgeSource> Sources { get; set; }

        [Required]
        public string UserId { get; set; }
        public DateTime LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
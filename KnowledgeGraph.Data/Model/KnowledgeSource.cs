using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeSource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public KnowledgeSourceType Type { get; set; }
        public ICollection<KnowledgeAuthorSource> AuthorSource { get; set; }
        public string Comment { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime LastModificationTime { get; set; }

        public IList<KnowledgeContent> Contents { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

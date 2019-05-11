using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Data.Model
{
    public class KnowledgeAuthor
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Comment { get; set; }
        public IEnumerable<KnowledgeAuthorSource> AuthorSource { get; set; }

        [Required]
        public DateTime CreationTime { get; set; }

        [Required]
        public DateTime LastModificationTime { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
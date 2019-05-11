using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels
{
    public class DetailsKnowledgeSourceViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Source type")]
        public KnowledgeSourceTypeForSourceViewModel SourceType { get; set; }

        public IEnumerable<KnowledgeAuthorForSourceViewModel> Authors { get; set; }

        [Display(Name = "Creation time")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Last modification time")]
        public DateTime LastModificationTime { get; set; }

        [Display(Name="Comment")]
        public string Comment { get; set; }
    }
}

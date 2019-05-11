using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels
{
    public class DetailsKnowledgeSourceTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Creation time")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Last modification time")]
        public DateTime LastModificationTime { get; set; }

        public IEnumerable<KnowledgeSourceForSourceTypeViewModel> Sources { get; set; }
    }
}

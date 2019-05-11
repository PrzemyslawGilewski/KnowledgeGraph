using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels
{
    public class ListKnowledgeSourceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Source type")]
        public KnowledgeSourceTypeForSourceViewModel SourceType { get; set; }

        [Display(Name = "Authors")]
        public IEnumerable<KnowledgeAuthorForSourceViewModel> AuthorList { get; set; }

        [Display(Name = "Usage count")]
        public int UsageCount { get; set; }
    }
}

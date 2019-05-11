using KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels
{
    public class CreateKnowledgeSourceViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public IList<ListKnowledgeSourceTypeViewModel> SourceTypes { get; set; }

        [Display(Name="Source type")]
        public int SourceTypeId { get; set; }
        public IEnumerable<KnowledgeAuthorForSourceViewModel> Authors { get; set; }

        [Display(Name = "Authors")]
        public List<int> AuthorIds { get; set; }

        [Display(Name="Comment")]
        public string Comment { get; set; }

    }
}

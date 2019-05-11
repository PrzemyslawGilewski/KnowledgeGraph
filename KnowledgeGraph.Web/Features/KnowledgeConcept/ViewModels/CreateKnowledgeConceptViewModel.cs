using KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels
{
    public class CreateKnowledgeConceptViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
        public int CategoryId { get; set; }

        [Display(Name="Category")]
        public IEnumerable<KnowledgeCategoryViewModel> Categories { get; set; }
    }
}

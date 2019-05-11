using KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels
{
    public class DetailsKnowledgeConceptViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public IList<KnowledgeContentViewModel> Contents { get; set; }
    }
}

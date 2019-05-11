using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels
{
    public class DetailsKnowledgeCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<KnowledgeConceptForDetailsKnowledgeCategoryViewModel> KnowledgeConcepts { get; set; }
    }
}

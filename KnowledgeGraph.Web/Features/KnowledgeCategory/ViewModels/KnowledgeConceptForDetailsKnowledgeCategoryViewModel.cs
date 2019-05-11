using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels
{
    public class KnowledgeConceptForDetailsKnowledgeCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }
    }
}

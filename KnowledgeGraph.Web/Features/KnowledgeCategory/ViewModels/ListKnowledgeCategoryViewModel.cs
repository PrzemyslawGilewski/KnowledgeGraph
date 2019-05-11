using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels
{
    public class ListKnowledgeCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Usage count")]
        public int UsageCount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels
{
    public class ListKnowledgeSourceTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name= "Usage count")]
        public int UsageCount { get; set; }
    }
}
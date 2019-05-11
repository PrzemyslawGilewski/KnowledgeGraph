using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels
{
    public class KnowledgeCategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
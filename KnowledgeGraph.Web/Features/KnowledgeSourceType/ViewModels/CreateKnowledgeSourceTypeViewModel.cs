using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels
{
    public class CreateKnowledgeSourceTypeViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}

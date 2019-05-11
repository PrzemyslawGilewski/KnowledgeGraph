using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels
{
    public class EditKnowledgeSourceTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}

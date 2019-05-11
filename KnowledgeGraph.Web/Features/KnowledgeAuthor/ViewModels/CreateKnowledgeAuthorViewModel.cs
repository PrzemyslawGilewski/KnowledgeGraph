using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeAuthor.ViewModels
{
    public class CreateKnowledgeAuthorViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}

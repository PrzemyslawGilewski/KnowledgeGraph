using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels
{
    public class CreateKnowledgeContentViewModel
    {
        public string ConceptName { get; set; }

        [Display(Name = "Concept")]
        public int ConceptId { get; set; }

        public List<SelectListItem> CategoriesWithConcepts { get; set; }
        
        [Display(Name = "Source")]
        public int SourceId { get; set; }
        public IEnumerable<KnowledgeSourceViewModel> Sources { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        public bool Attached { get; set; }

        public string UserId { get; set; }
    }
}

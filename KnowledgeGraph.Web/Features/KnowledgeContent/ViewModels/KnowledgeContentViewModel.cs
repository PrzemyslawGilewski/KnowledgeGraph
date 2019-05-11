using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels
{
    public class KnowledgeContentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Source")]
        public KnowledgeSourceViewModel Source { get; set; }

        [Display(Name = "Creation time")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last modification time")]
        public DateTime LastModificationDate { get; set; }
    }
}

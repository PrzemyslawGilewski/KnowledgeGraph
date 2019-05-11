using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Web.Features.KnowledgeAuthor.ViewModels
{
    public class DetailsKnowledgeAuthorViewModel
    {
        public int Id { get; set; }

        [Display(Name= "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Source count")]
        public int SourceCount { get; set; }

        [Display(Name = "Creation time")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last modification time")]
        public DateTime LastModificationDate { get; set; }
        public IEnumerable<KnowledgeSourceViewModel> Sources { get; set; }

    }
}
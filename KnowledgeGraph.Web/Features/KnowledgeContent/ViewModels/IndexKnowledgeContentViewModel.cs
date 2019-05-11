using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using System.Collections.Generic;

namespace KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels
{
    public class IndexKnowledgeContentViewModel
    {
        public KnowledgeSourceViewModel Source { get; set; }
        public IEnumerable<KnowledgeContentViewModel> KnowledgeContentViewModels { get; set; }
    }
}

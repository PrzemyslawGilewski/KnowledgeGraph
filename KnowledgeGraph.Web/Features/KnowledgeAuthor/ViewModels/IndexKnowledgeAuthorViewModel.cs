using System.Collections.Generic;

namespace KnowledgeGraph.Web.Features.KnowledgeAuthor.ViewModels
{
    public class IndexKnowledgeAuthorViewModel
    {
        public IEnumerable<KnowledgeAuthorViewModel> Authors { get; set; }
    }
}

using System.Collections.Generic;

namespace KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels
{
    public class IndexKnowledgeConceptViewModel
    {
        public IEnumerable<KnowledgeCategoryWithConceptsViewModel> Categories { get; set; }
    }
}

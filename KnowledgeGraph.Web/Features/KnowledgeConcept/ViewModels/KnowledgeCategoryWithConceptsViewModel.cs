using KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels;
using System.Collections.Generic;

namespace KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels
{
    public class KnowledgeCategoryWithConceptsViewModel
    {
        public KnowledgeCategoryViewModel Category { get; set; }

        public IEnumerable<KnowledgeConceptSimpleViewModel> KnowledgeConcepts { get; set; }
    }
}

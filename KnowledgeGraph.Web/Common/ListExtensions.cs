using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Common
{
    public static class ListExtensions
    {
        public static bool HasAnyItems<T>(this IEnumerable<T> list)
        {
            return list.Count() > 0;
        }
    }
}

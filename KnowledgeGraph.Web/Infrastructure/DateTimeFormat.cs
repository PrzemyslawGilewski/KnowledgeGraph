using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Infrastructure
{
    public static class DateTimeFormat
    {
        public static string Format(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss dd/MM/yyyy");
        }
    }
}

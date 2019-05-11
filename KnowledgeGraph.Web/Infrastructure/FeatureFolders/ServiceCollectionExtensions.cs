using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeGraph.Web.Infrastructure.FeatureFolders
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddRazorOptions(o =>
            {
                o.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
            });

            return services;
        }
    }
}

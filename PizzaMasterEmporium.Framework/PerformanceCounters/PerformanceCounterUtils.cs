using System.Diagnostics;

namespace PizzaMasterEmporium.Framework.PerformanceCounters
{
    public class PerformanceCounterUtils
    {
        public static void CreatePerformanceCounter(string categoryName, string categoryHelp,
            CounterCreationDataCollection counterCreationDataCollection)
        {
            if (!PerformanceCounterCategory.Exists(categoryName))
            {   
                // create new category with the counters above
                PerformanceCounterCategory.
                    Create(categoryName, categoryHelp,
                           PerformanceCounterCategoryType.SingleInstance,
                           counterCreationDataCollection);
            }
        }
    }
}

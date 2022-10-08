using System.Linq.Dynamic.Core;
using System.Text;

namespace OniCore.Persistence.Dynamic
{
    public static class DynamicExtensions
    {
        private static readonly Dictionary<string, string> _operators = new()
        {
            { "eq", "=" },
            { "neq", "!=" },
            { "lt", "<" },
            { "lte", "<=" },
            { "gt", ">" },
            { "gte", ">=" },

            { "startswith", "StartsWith" },
            { "endswith", "EndsWith" },
            { "contains", "Contains" },
            { "notcontains", "Contains" },

            { "isnull", "== null" },
            { "isnotnull", "!= null" }
        };

        public static IQueryable<T> Filter<T>(this IQueryable<T> source, Filter filter)
        {
            IList<Filter> allFilters = GetAllFilters(filter);
            string[] values = allFilters.Select(x => x.Value).ToArray();
            string where = TransformFilter(allFilters, filter);
            return source.Where(where, values);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> source, IEnumerable<Sort> sorts)
        {
            string ordering = string.Join(',', sorts.Select(x => $"{x.Field} {x.Direction}"));
            return source.OrderBy(ordering);
        }

        private static IList<Filter> GetAllFilters(Filter filter)
        {
            List<Filter> filters = new();
            GetFilters(filters, filter);
            return filters;
        }

        private static void GetFilters(IList<Filter> filters, Filter filter)
        {
            filters.Add(filter);

            if (filter.Filters != null)
                foreach (Filter item in filter.Filters)
                    GetFilters(filters, item);
        }

        private static string TransformFilter(IList<Filter> filters, Filter filter)
        {
            int index = filters.IndexOf(filter);
            string @operator = _operators[filter.Operator];
            StringBuilder where = new();

            if (!string.IsNullOrEmpty(filter.Value))
            {
                if (filter.Operator == "startswith"
                 || filter.Operator == "endswith"
                 || filter.Operator == "contains")
                {
                    where.Append($"np({filter.Field}).{@operator}(@{index})");
                }
                else if (filter.Operator == "notcontains")
                {
                    where.Append($"!np({filter.Field}).{@operator}(@{index})");
                }
                else
                    where.Append($"np({filter.Field}) {@operator} @{index}");
            }
            else if (filter.Operator == "isnull" || filter.Operator == "isnotnull")
            {
                where.Append($"np({filter.Field}) {@operator}");
            }

            if (filter.Logic != null && filter.Filters != null && filter.Filters.Any())
            {
                IEnumerable<string> transformedFilters = filter.Filters.Select(f => TransformFilter(filters, f));
                return $"{where} {filter.Logic} ({string.Join($"{filter.Logic}", transformedFilters)})";
            }
            return where.ToString();
        }
    }
}

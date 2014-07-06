using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cayley.Net.Dsl.Gremlin.Steps
{
    public static class BasicSteps
    {
        private const string BOTH_V = ".bothV";
        private const string OUT_V = ".outV";
        private const string IN_V = ".inV";
        private const string BOTH_E = ".bothE";
        private const string OUT_E = ".outE";
        private const string IN_E = ".inE";
        private const string BOTH = ".both({0})";
        private const string OUT = ".Out({0})";
        private const string IN = ".in({0})";

        public static IGremlinQuery BothV(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(BOTH_V);
            return newQuery;
        }
        public static IGremlinQuery BothV(this IGremlinQuery query, IEnumerable<Filter> filters,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddFilterBlock(BOTH_V, filters, comparison);
            return newQuery;
        }


        public static IGremlinQuery OutV(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(OUT_V);
            return newQuery;
        }

        public static IGremlinQuery OutV(this IGremlinQuery query, IEnumerable<Filter> filters,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddFilterBlock(OUT_V, filters, comparison);
            return newQuery;
        }

        public static IGremlinQuery InV(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(IN_V);
            return newQuery;
        }

        public static IGremlinQuery InV(this IGremlinQuery query, IEnumerable<Filter> filters,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddFilterBlock(IN_V, filters, comparison);
            return newQuery;
        }


        public static IGremlinQuery BothE(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(BOTH_E);
            return newQuery;
        }


        public static IGremlinQuery BothE(this IGremlinQuery query, string label)
        {
            Filter filter = GetFilter(label);

            IGremlinQuery newQuery = query.AddFilterBlock(BOTH_E, new[] {filter}, StringComparison.Ordinal);
            return newQuery;
        }


        public static IGremlinQuery BothE(this IGremlinQuery query,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddFilterBlock(BOTH_E, filters, comparison);
            return newQuery;
        }

        public static IGremlinQuery BothE(this IGremlinQuery query, string label,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            Filter filter = GetFilter(label);

            filters = filters.Concat(new[] {filter});

            IGremlinQuery newQuery = query.AddFilterBlock(BOTH_E, filters, comparison);
            return newQuery;
        }


        public static IGremlinQuery OutE(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(OUT_E);
            return newQuery;
        }

        public static IGremlinQuery OutE(this IGremlinQuery query, string label)
        {
            Filter filter = GetFilter(label);

            IGremlinQuery newQuery = query.AddFilterBlock(OUT_E, new[] {filter}, StringComparison.Ordinal);
            return newQuery;
        }


        public static IGremlinQuery OutE(this IGremlinQuery query, IEnumerable<Filter> filters,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddFilterBlock(OUT_E, filters, comparison);
            return newQuery;
        }

        public static IGremlinQuery OutE<TData>(this IGremlinQuery query,
            Expression<Func<TData, bool>> filter, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            where TData : class, new()
        {
            IEnumerable<Filter> filters = FilterFormatters.TranslateFilter(filter);
            return query.OutE(filters, comparison);
        }


        public static IGremlinQuery OutE(this IGremlinQuery query, string label,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            Filter filter = GetFilter(label);

            filters = filters.Concat(new[] {filter});

            IGremlinQuery newQuery = query.AddFilterBlock(OUT_E, filters, comparison);
            return newQuery;
        }


        public static IGremlinQuery OutE<TData>(this IGremlinQuery query, string label,
            Expression<Func<TData, bool>> filter, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
            where TData : class, new()
        {
            IEnumerable<Filter> filters = FilterFormatters.TranslateFilter(filter);
            return query.OutE(label, filters, comparison);
        }

        public static IGremlinQuery InE(this IGremlinQuery query)
        {
            IGremlinQuery newQuery = query.AddBlock(IN_E);
            return newQuery;
        }
        public static IGremlinQuery InE(this IGremlinQuery query, string label)
        {
            Filter filter = GetFilter(label);

            IGremlinQuery newQuery = query.AddFilterBlock(IN_E, new[] {filter}, StringComparison.Ordinal);
            return newQuery;
        }

        public static IGremlinQuery In(this IGremlinQuery query, string label)
        {
            IGremlinQuery newQuery = query.AddBlock(IN, label);
            return newQuery;
        }


        public static IGremlinQuery Both(this IGremlinQuery query, string label)
        {
            IGremlinQuery newQuery = query.AddBlock(BOTH, label);
            return newQuery;
        }

        public static IGremlinQuery Both(this IGremlinQuery query, string label,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddBlock(BOTH, label);
            IGremlinQuery filterQuery = newQuery.AddFilterBlock(string.Empty, filters, comparison);
            return filterQuery;
        }


        public static IGremlinQuery Both<TNode>(this IGremlinQuery query, string label,
            Expression<Func<TNode, bool>> filter, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddBlock(BOTH, label);
            IEnumerable<Filter> filters = FilterFormatters.TranslateFilter(filter);
            IGremlinQuery filterQuery = newQuery.AddFilterBlock(string.Empty, filters, comparison);
            return filterQuery;
        }

        public static IGremlinQuery Out(this IGremlinQuery query, string label)
        {
            IGremlinQuery newQuery = query.AddBlock(OUT, label);
            return newQuery;
        }
        public static IGremlinQuery Out(this IGremlinQuery query, string label,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddBlock(OUT, label);
            IGremlinQuery filterQuery = newQuery.AddFilterBlock(string.Empty, filters, comparison);
            return filterQuery;
        }

        public static IGremlinQuery In(this IGremlinQuery query, string label,
            IEnumerable<Filter> filters, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddBlock(IN, label);
            IGremlinQuery filterQuery = newQuery.AddFilterBlock(string.Empty, filters, comparison);
            return filterQuery;
        }

        public static IGremlinQuery In<TNode>(this IGremlinQuery query, string label,
            Expression<Func<TNode, bool>> filter, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            IGremlinQuery newQuery = query.AddBlock(IN, label);
            IEnumerable<Filter> filters = FilterFormatters.TranslateFilter(filter);
            IGremlinQuery filterQuery = newQuery.AddFilterBlock(string.Empty, filters, comparison);
            return filterQuery;
        }
        public static int GremlinCount(this IGremlinQuery query)
        {
            string queryText = string.Format("{0}.count()", query.QueryText);


            int result = 0;

            return result;
        }

        private static Filter GetFilter(string label)
        {
            // TODO: This filter should always be case sensitive, irrespective of how the rest are compared
            var filter = new Filter
            {
                ExpressionType = ExpressionType.Equal,
                PropertyName = "label",
                Value = label
            };
            return filter;
        }
    }
}
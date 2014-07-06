using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Cayley.Net.Dsl.Gremlin
{
    public static class GremlinQueryExtensions
    {
        private static readonly Regex ParameterReferenceRegex = new Regex(@"(?<=\W)p\d+(?!\w)");

        public static IGremlinQuery PrependVariablesToBlock(this IGremlinQuery baseQuery, IGremlinQuery query)
        {
            string declarations = query
                .QueryDeclarations
                .Aggregate(string.Empty,
                    (current, declaration) =>
                        !baseQuery.QueryText.Contains(declaration) ? declaration + current : current);
            return new GremlinQuery(declarations + baseQuery.QueryText, baseQuery.QueryParameters,
                query.QueryDeclarations);
        }

        public static IGremlinQuery AddBlock(this IGremlinQuery baseQuery, string text, params object[] parameters)
        {
            var paramsDictionary = new Dictionary<string, object>(baseQuery.QueryParameters);
            int nextParameterIndex = baseQuery.QueryParameters.Count;
            var paramNames = new List<string>();
            foreach (object paramValue in parameters)
            {
                string paramName = string.Format("p{0}", nextParameterIndex);
                paramNames.Add(paramName);
                paramsDictionary.Add(paramName, paramValue);
                nextParameterIndex++;
            }

            string textWithParamNames = parameters.Any()
                ? string.Format(text, paramNames.ToArray())
                : text;

            return new GremlinQuery(baseQuery.QueryText + textWithParamNames, paramsDictionary,
                baseQuery.QueryDeclarations);
        }

        public static IGremlinQuery AddCopySplitBlock(this IGremlinQuery baseQuery, string text, IGremlinQuery[] queries)
        {
            var declarations = new List<string>();
            string rootQuery = baseQuery.QueryText;
            var inlineQueries = new List<string>();

            var paramsDictionary = new Dictionary<string, object>(baseQuery.QueryParameters);
            int nextParameterIndex = baseQuery.QueryParameters.Count;

            foreach (IGremlinQuery query in queries)
            {
                string modifiedQueryText = RebuildParametersAndDeclarations(query, paramsDictionary, declarations,
                    ref nextParameterIndex, ref rootQuery);
                inlineQueries.Add(modifiedQueryText);
            }

            string splitBlockQueries = string.Format(text, inlineQueries.ToArray());

            return new GremlinQuery(rootQuery + splitBlockQueries, paramsDictionary, declarations);
        }

        public static IGremlinQuery AddIfThenElseBlock(this IGremlinQuery baseQuery, string ifThenElseText,
            IGremlinQuery ifExpression, IGremlinQuery ifThen, IGremlinQuery ifElse)
        {
            var declarations = new List<string>();
            string rootQuery = baseQuery.QueryText;
            var paramsDictionary = new Dictionary<string, object>(baseQuery.QueryParameters);
            int nextParameterIndex = baseQuery.QueryParameters.Count;

            string modifiedQueryTextifExpression = RebuildParametersAndDeclarations(ifExpression, paramsDictionary,
                declarations, ref nextParameterIndex, ref rootQuery);
            string modifiedQueryTextifThen = RebuildParametersAndDeclarations(ifThen, paramsDictionary, declarations,
                ref nextParameterIndex, ref rootQuery);
            string modifiedQueryTextifElse = RebuildParametersAndDeclarations(ifElse, paramsDictionary, declarations,
                ref nextParameterIndex, ref rootQuery);

            string newQueryText = string.Format(ifThenElseText, modifiedQueryTextifExpression, modifiedQueryTextifThen,
                modifiedQueryTextifElse);

            return new GremlinQuery(rootQuery + newQueryText, paramsDictionary, declarations);
        }

        public static IGremlinQuery AddFilterBlock(this IGremlinQuery baseQuery, string text,
            IEnumerable<Filter> filters, StringComparison comparison)
        {
            FormattedFilter formattedFilter = FilterFormatters.FormatGremlinFilter(filters, comparison, baseQuery);

            string newQueryText = baseQuery.QueryText + text + formattedFilter.FilterText;

            var newParams = new Dictionary<string, object>(baseQuery.QueryParameters);
            foreach (string key in formattedFilter.FilterParameters.Keys)
                newParams.Add(key, formattedFilter.FilterParameters[key]);

            return new GremlinQuery(newQueryText, newParams, baseQuery.QueryDeclarations);
        }

        public static string ToQueryText(this IGremlinQuery query)
        {
            string text = query.QueryText;
            if (query.QueryParameters == null) return text;
            foreach (string key in query.QueryParameters.Keys.Reverse())
            {
                text = text.Replace(key, string.Format("'{0}'", query.QueryParameters[key]));
            }
            return text;
        }

        private static string RebuildParametersAndDeclarations(IGremlinQuery query,
            Dictionary<string, object> paramsDictionary,
            List<string> declarations, ref int nextParamaterIndex, ref string rootQuery)
        {
            int updatedIndex = nextParamaterIndex;
            var paramNames = new List<string>();

            declarations.AddRange(query.QueryDeclarations);
            string modifiedQueryText = ParameterReferenceRegex.Replace(
                query.QueryText,
                m =>
                {
                    int parameterIndex = updatedIndex;
                    updatedIndex++;

                    string oldParamKey = m.Value;
                    string newParamKey = string.Format("p{0}", parameterIndex);

                    paramNames.Add(newParamKey);
                    paramsDictionary.Add(newParamKey, query.QueryParameters[oldParamKey]);

                    return newParamKey;
                });

            nextParamaterIndex = updatedIndex;

            foreach (string declareStatement in query.QueryDeclarations)
            {
                rootQuery = declareStatement + rootQuery;
                modifiedQueryText = modifiedQueryText.Replace(declareStatement, string.Empty);
            }
            return modifiedQueryText;
        }
    }
}
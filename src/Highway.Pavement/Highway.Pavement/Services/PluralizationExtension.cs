using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Highway.Pavement.Services
{
    public static class Formatting
    {
        private static IList<string> NonPluralizables = new List<string> { "equipment", "information", "rice", "money", "species", "series", "fish", "sheep", "deer" };
        private static IDictionary<string, string> Pluralizations = new Dictionary<string, string>
        {
            { "(.*)fe?", "$1ves" },         // ie, wolf, wife
            { "(.*)man$", "$1men" },        // ie, workman, workmen
            { "(.+[aeiou]y)$", "$1s" },     
            { "(.+[^aeiou])y$", "$1ies" },
            { "(.+z)$", "$1zes" },
            { "([m|l])ouse$", "$1ice" },
            { "(.+)(e|i)x$", @"$1ices"},    // ie, Matrix, Index
            { "(octop|vir)us$", "$1i"},
            { "(.+(s|x|sh|ch))$", @"$1es"},
            { "(.+)", @"$1s" }
        };

        private static IDictionary<string, string> SpecificCases = new Dictionary<string, string>()
        {
            { "person", "people" },
            { "ox", "oxen" },
            { "child", "children" },
            { "foot", "feet" },
            { "tooth", "teeth" },
            { "goose", "geese" },
            { "dogma", "dogmata"},
            { "schema", "schemata"},
        };

        public static void AddPluralization(string singular, string plural)
        {
            if (SpecificCases.ContainsKey(singular))
            {
                SpecificCases[singular] = plural;
            }
            else
            {
                SpecificCases.Add(new KeyValuePair<string, string>(singular, plural));
            }
        }

        public static void AddPluralizationPattern(string singularPattern, string pluralPattern)
        {
            if (SpecificCases.ContainsKey(singularPattern))
            {
                SpecificCases[singularPattern] = pluralPattern;
            }
            else
            {
                SpecificCases.Add(new KeyValuePair<string, string>(singularPattern, pluralPattern));
            }
        }

        public static void AddNonPluralizable(string singular)
        {
            if (NonPluralizables.Contains(singular)) return;
            NonPluralizables.Add(singular);
        }

        public static string Pluralize(this string singular)
        {
            if (SpecificCases.ContainsKey(singular))
            {
                return SpecificCases[singular];
            }

            if (NonPluralizables.Contains(singular))
                return singular;

            var plural = "";

            foreach (var pluralization in Pluralizations)
            {
                if (Regex.IsMatch(singular, pluralization.Key))
                {
                    plural = Regex.Replace(singular, pluralization.Key, pluralization.Value);
                    break;
                }
            }

            return plural;
        }
    }
}
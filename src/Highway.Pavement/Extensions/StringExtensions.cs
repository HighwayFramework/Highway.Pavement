using System.Collections.Generic;

namespace System
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string Format(this string inputFormat, params object[] args)
        {
            return string.Format(inputFormat, args);
        }

        private static readonly Dictionary<Type, Delegate> Parsers = CreateParsers();

        private static Dictionary<Type, Delegate> CreateParsers()
        {
            var parsers = new Dictionary<Type, Delegate>();
            AddParser<DateTime>(parsers, DateTime.TryParse);
            AddParser<Int16>(parsers, Int16.TryParse);
            AddParser<Int32>(parsers, Int32.TryParse);
            AddParser<Int64>(parsers, Int64.TryParse);
            AddParser<UInt16>(parsers, UInt16.TryParse);
            AddParser<UInt32>(parsers, UInt32.TryParse);
            AddParser<UInt64>(parsers, UInt64.TryParse);
            AddParser<Decimal>(parsers, Decimal.TryParse);
            AddParser<Double>(parsers, Double.TryParse);
            AddParser<float>(parsers, float.TryParse);
            return parsers;
        }

        static void AddParser<T>(Dictionary<Type, Delegate> parsers, Parser<T> parser)
        {
            parsers[typeof(T)] = parser;
        }

        private delegate bool Parser<T>(string value, out T result);

        public static T Parse<T>(this string value) where T : struct
        {
            Delegate del;
            if (Parsers.TryGetValue(typeof (T), out del))
            {
                Parser<T> parser = (Parser<T>) del;
                T result;
                parser(value, out result);
                return result;

            }
           throw new NotImplementedException("The Type {0} is not supported for this extension, if you have a new type that you would like to add please submit it on github.");
        }
    }
}
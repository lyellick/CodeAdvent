using CodeAdvent.Common.Models;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common.Extensions
{
    public static class CodeAdventDataExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventEvent codeAdventEvent)
        {
            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return (T)Convert.ChangeType(line, typeof(T)); ;
        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventEvent codeAdventEvent, Func<string, T> action)
        {
            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return action(line);

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventEvent codeAdventEvent, string pattern, Func<Match, T> action)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                foreach (Match match in find.Matches(line).Cast<Match>())
                    yield return action(match);
        }
    }
}

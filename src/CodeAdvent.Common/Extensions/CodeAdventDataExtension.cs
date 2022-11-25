using CodeAdvent.Common.Models;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common.Extensions
{
    public static class CodeAdventDataExtension
    {
        public static IEnumerable<T> Map<T>(this CodeAdventEvent codeAdventEvent, string pattern, Func<Match, T> action)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                foreach (Match match in find.Matches(line))
                    yield return action(match);
        }

        public static IEnumerable<string> ToEnumerable(this CodeAdventEvent codeAdventEvent)
        {
            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return line;
        }
    }
}

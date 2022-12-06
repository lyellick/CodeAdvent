using CodeAdvent.Common.Models;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common.Extensions
{
    public static class CodeAdventDataExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent)
        {
            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return (T)Convert.ChangeType(line, typeof(T)); ;
        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, Func<string, T> action)
        {
            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return action(line);

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string pattern, Func<Match, T> match)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return match(find.Match(line));

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string pattern, Func<Match,bool,T> match)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return match(find.Match(line), find.IsMatch(line));

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string pattern, Func<MatchCollection, T> matches)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                    yield return matches(find.Matches(line));
        }
    }
}

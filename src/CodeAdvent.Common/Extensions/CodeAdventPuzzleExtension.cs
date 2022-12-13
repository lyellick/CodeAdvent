using CodeAdvent.Common.Models;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common.Extensions
{
    public static class CodeAdventPuzzleExtension
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

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string split, Func<string, T> action)
        {
            var groups = codeAdventEvent.Input.Split(split);

            foreach (var group in groups)
                yield return action(group);

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string pattern, Func<Match, T> match)
        {
            Regex find = new(pattern);

            using var reader = new StringReader(codeAdventEvent.Input);

            for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                yield return match(find.Match(line));

        }

        public static IEnumerable<T> ToEnumerable<T>(this CodeAdventPuzzle codeAdventEvent, string split, string pattern, Func<Match, T> match)
        {
            Regex find = new(pattern);

            var groups = codeAdventEvent.Input.Split(split);

            foreach (var group in groups)
                yield return match(find.Match(group));

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

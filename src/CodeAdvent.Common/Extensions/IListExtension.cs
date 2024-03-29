﻿using CodeAdvent.Common.Models;

namespace CodeAdvent.Common.Extensions
{
    public static class IListExtension
    {
        /// <summary>
        /// Generate all permutations of a given list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<IList<T>> GeneratePermutations<T>(this IList<T> source)
        {
            int length = source.Count;
            var indices = new int[length];
            var variations = new HashSet<T>();

            while (indices[0] < source.Count)
            {
                var variation = new List<T>(length);
                var valid = true;

                for (int i = 0; i < length; ++i)
                {
                    var element = source[indices[i]];
                    if (variations.Contains(element))
                    {
                        valid = false;
                        break;
                    }
                    variation.Add(element);
                    variations.Add(element);
                }

                if (valid)
                    yield return variation.ToArray();

                indices[length - 1]++;

                for (int i = length - 1; i > 0; --i)
                {
                    if (indices[i] >= source.Count)
                    {
                        indices[i] = 0;
                        indices[i - 1]++;
                    }
                    else
                        break;
                }

                variations.Clear();
            }
        }

        /// <summary>
        /// Generate splits of a given list with each group by bring n size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<IList<T>> ToSplits<T>(this IList<T> source, int size) =>
            source.Select((item, index) => new { Index = index, Value = item })
                  .GroupBy(item => item.Index / size)
                  .Select(group => group.Select(item => item.Value).ToArray());

        public static IEnumerable<IList<T>> ToSplit<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
                yield return array.Skip(i * size).Take(size).ToArray();
        }

        public static T[][] ToSlidingSplit<T>(this IList<T> source, int size)
        {
            List<T[]> container = new();

            for (int i = 0; i < source.Count; i++)
            {
                var split = new List<T>();
                if (i < source.Count - size)
                {
                    for (int n = 0; n < size; n++)
                    {
                        split.Add(source[i + n]);
                    }
                }
                container.Add(split.ToArray());
            }

            return container.ToArray();
        }

        public static NavigationList<T> ToNavigationList<T>(this IList<T> source)
        {
            NavigationList<T> output = new();
            
            output.AddRange(source);

            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAdvent.Common.Extensions
{
    public static class IListExtension
    {
        public static IEnumerable<IList<T>> GeneratePermutations<T>(this IList<T> source)
        {
            int length = source.Count;

            var startIndices = new int[length];
            var variationElements = new HashSet<T>();

            while (startIndices[0] < source.Count)
            {
                var variation = new List<T>(length);
                var valid = true;
                for (int i = 0; i < length; ++i)
                {
                    var element = source[startIndices[i]];
                    if (variationElements.Contains(element))
                    {
                        valid = false;
                        break;
                    }
                    variation.Add(element);
                    variationElements.Add(element);
                }
                if (valid)
                    yield return variation;

                startIndices[length - 1]++;
                for (int i = length - 1; i > 0; --i)
                {
                    if (startIndices[i] >= source.Count)
                    {
                        startIndices[i] = 0;
                        startIndices[i - 1]++;
                    }
                    else
                        break;
                }
                variationElements.Clear();
            }
        }
    }
}

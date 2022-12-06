using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CodeAdvent.Common.Extensions
{
    public static class GroupCollectionExtension
    {
        public static IEnumerable<T> ToEnumerable<T>(this GroupCollection groups, bool skipFirst = true)
        {
            for (int i = skipFirst ? 1 : 0; i < groups.Count; i++)
                if (TypeDescriptor.GetConverter(typeof(T)).IsValid(groups[i].Value))
                    yield return (T)Convert.ChangeType(groups[i].Value, typeof(T));
        }

        public static IEnumerable<T> ToEnumerable<T>(this GroupCollection groups, Func<string, T> action, bool skipFirst = true)
        {
            for (int i = skipFirst ? 1 : 0; i < groups.Count; i++)
                yield return action(groups[i].Value);
        }
    }
}

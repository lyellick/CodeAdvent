using Newtonsoft.Json.Linq;

namespace CodeAdvent.Common.Extensions
{
    public static class JArrayExtension
    {
        public static bool? CompareTo(this JArray compare, JArray to)
        {
            for (int i = 0; i < compare.Count; i++)
            {
                if (to.Count <= i)
                    return false;

                var left = compare[i];
                var right = to[i];

                if (left.Type == JTokenType.Integer && right.Type == JTokenType.Integer)
                {
                    int li = left.ToObject<int>(), ri = right.ToObject<int>();

                    if (li < ri)
                        return true;
                    else if (ri > li) 
                        return false;

                    continue;
                }

                if (left.Type  == JTokenType.Integer)
                    left = new JArray(left);
                else if (right.Type == JTokenType.Integer)
                    right = new JArray(right);

                var result = (left as JArray).CompareTo(right as JArray);

                if (result != null)
                    return null;
            }

            if (to.Count == compare.Count)
                return null;

            return true;
        }
    }
}

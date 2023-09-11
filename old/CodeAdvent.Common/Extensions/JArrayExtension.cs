using Newtonsoft.Json.Linq;

namespace CodeAdvent.Common.Extensions
{
    public static class JArrayExtension
    {
        public static bool? CompareTo(this JArray from, JArray to)
        {
            for (int i = 0; i < from.Count; i++)
            {
                if (to.Count <= i)
                    return false;

                JToken left = from[i], right = to[i];

                switch (left.Type)
                {
                    case JTokenType.Integer when right.Type == JTokenType.Integer:
                        int li = left.ToObject<int>(), ri = right.ToObject<int>();

                        if (li < ri)
                            return true;

                        if (ri < li)
                            return false;

                        continue;
                    case JTokenType.Integer:
                        left = new JArray(left);
                        break;
                    default:
                        if (right.Type == JTokenType.Integer)
                            right = new JArray(right);
                        break;
                }

                var result = (left as JArray).CompareTo(right as JArray);

                if (result != null)
                    return result;
            }

            if (to.Count == from.Count)
                return null;

            return true;
        }
    }
}

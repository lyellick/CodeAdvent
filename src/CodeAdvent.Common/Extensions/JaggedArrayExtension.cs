namespace CodeAdvent.Common.Extensions
{
    public static class JaggedArrayExtension
    {
        public static List<List<T>> Pivot<T>(this List<List<T>> jarray)
        {
            var output = new List<List<T>>();
            for (int col = 0; col < jarray[0].Count; col++)
                if (typeof(T) == typeof(string))
                {
                    output.Add(Enumerable.Range(0, jarray.Count).Select(i => jarray[i][col]).Where(container => !string.IsNullOrWhiteSpace(container.ToString())).ToList());
                }
                else
                {
                    output.Add(Enumerable.Range(0, jarray.Count).Select(i => jarray[i][col]).ToList());
                }

            return output;
        }
    }
}

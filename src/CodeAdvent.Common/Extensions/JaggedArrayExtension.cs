﻿using System.Runtime.CompilerServices;

namespace CodeAdvent.Common.Extensions
{
    public static class JaggedArrayExtension
    {
        public static List<List<T>> ToPivot<T>(this List<List<T>> jarray)
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

        public static T[][] ToPivot<T>(this T[][] jarray)
        {
            var output = new List<T[]>();
            for (int col = 0; col < jarray[0].Length; col++)
                if (typeof(T) == typeof(string))
                {
                    output.Add(Enumerable.Range(0, jarray.Length).Select(i => jarray[i][col]).Where(container => !string.IsNullOrWhiteSpace(container.ToString())).ToArray());
                }
                else
                {
                    output.Add(Enumerable.Range(0, jarray.Length).Select(i => jarray[i][col]).ToArray());
                }

            return output.ToArray();
        }

        public static bool IsWithinBounds<T>(this T[][] jarray, int row, int col) => row < jarray.Length && row >= 0 && col < jarray[0].Length && col >= 0;
    }
}

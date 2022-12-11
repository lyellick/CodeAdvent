namespace CodeAdvent.Common.Utilities
{
    public static class JaggedArrayUtility
    {
        public static T Create<T>(params int[] lengths) => (T)Initialize(typeof(T).GetElementType(), 0, lengths);

        private static object Initialize(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);

            Type element = type.GetElementType();

            if (element != null)
                for (int i = 0; i < lengths[index]; i++)
                    array.SetValue(Initialize(element, index + 1, lengths), i);

            return array;
        }
    }
}

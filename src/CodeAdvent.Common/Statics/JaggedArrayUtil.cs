namespace CodeAdvent.Common.Statics
{
    public static class JaggedArrayUtil
    {
        public static T Create<T>(params int[] lengths)
        {
            return (T)Initialize(typeof(T).GetElementType(), 0, lengths);
        }

        public static object Initialize(Type type, int index, int[] lengths)
        {
            Array array = Array.CreateInstance(type, lengths[index]);
            Type elementType = type.GetElementType();

            if (elementType != null)
            {
                for (int i = 0; i < lengths[index]; i++)
                {
                    array.SetValue(
                        Initialize(elementType, index + 1, lengths), i);
                }
            }

            return array;
        }
    }
}

namespace FunctionLibrary
{
    public static class GlobalFunctions
    {
        public static T[] CloneArrayByLoop<T>(this T[] array)
        {
            T[] result = new T[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                result[i] = array[i];
            }
            return result;
        }

        public static T[] CloneArrayByArrayCopy<T>(this T[] array)
        {
            T[] result = new T[array.Length];
            array.CopyTo(result, 0);
            return result;
        }

        public static T[] CloneArrayBySpanLoop<T>(this T[] array)
        {
            Span<T> result_span = new(new T[array.Length]);
            ReadOnlySpan<T> array_span = array.AsSpan();

            for(int i = 0; i < array_span.Length; i++)
            {
                result_span[i] = array_span[i];
            }

            return result_span.ToArray();
        }

        public static T[] CloneArrayBySpanCopy<T>(this T[] array)
        {
            T[] result = new T[array.Length];
            array.AsSpan().CopyTo(result);
            return result;
        }
    }
}

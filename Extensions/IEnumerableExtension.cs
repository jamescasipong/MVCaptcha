namespace MVCaptcha.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random? random = null)
        {
            random ??= new Random();

            return source.OrderBy(_ => random.Next());
        }
    }
}

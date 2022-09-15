namespace ChuckJokes.Infrastructure.Extensions
{
    public static class CacheHelpers
    {
        public static string GenerateToDoItemsCacheKey()
        {
            return "jokes";
        }
    }
}

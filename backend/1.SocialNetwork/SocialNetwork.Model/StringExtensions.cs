namespace SocialNetwork.Model
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        
        public static bool HasValue(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }
}
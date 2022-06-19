namespace BlazorMonaco.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToLowerCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length is 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        public static string ToMonarchType(this string str) =>
            $"@{str.FirstCharToLowerCase()}";
    }
}

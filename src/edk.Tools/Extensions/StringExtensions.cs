namespace edk.Tools.Extensions;

public static class StringExtensions
{
    public static bool ContainsUpper(this string text)
       => text.Any(char.IsUpper);

    public static bool ContainsLower(this string text)
       => text.Any(char.IsLower);

    public static bool ContainsDigit(this string text)
      => text.Any(char.IsDigit);

    public static bool ContainsSpecialCharacter(this string text)
    {
        var characters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";

        return characters.ToCharArray()
                        .Any(c => text.Contains(c));
    }
}

using System.Text;

namespace Squid.Prism.Engine.Core.Utils;

public static class StringUtils
{
    public static string ToSnakeCase(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        if (text.Length < 2)
        {
            return text.ToLowerInvariant();
        }

        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('_');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}

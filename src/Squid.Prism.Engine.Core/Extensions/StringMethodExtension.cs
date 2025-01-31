using Squid.Prism.Engine.Core.Utils;

namespace Squid.Prism.Engine.Core.Extensions;

public static class StringMethodExtension
{

    public static string ToSnakeCase(this string text)
    {
        return StringUtils.ToSnakeCase(text);
    }

}
